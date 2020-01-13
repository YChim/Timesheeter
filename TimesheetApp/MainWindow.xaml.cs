using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows;
using TimesheetApp.Helpers;
using TimesheetApp.Logic;
using TimesheetApp.Models;

namespace TimesheetApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeTaskService _timeTaskService;
        private PrimaryInformation _primaryInformation;
        private UserTimes _userTimes;
        private readonly UserData _userData;


        public MainWindow()
        {
            InitializeComponent();
            _userData = ReadUserData.ReadUserDataFromCsv();
            if (_userData != null)
            {
                PopulateExistingInformation();
            }
        }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            GetUserInformation();
        }

        public async void GetUserInformation()
        {
            try
            {
                _timeTaskService = new TimeTaskService(UserToken.Text);
                _primaryInformation = new PrimaryInformation(_timeTaskService);
                 var success = await _primaryInformation.GetMyInformation();
                if (success == false)
                {
                    MessageBox.Show("Authentication failed");
                }
                else
                {
                    Proceed();
                    MessageBox.Show("Successfully Authenticated");
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Authentication failed" + e.Message);
            }
            
        }

        private void PopulateExistingInformation()
        {
            Auth.IsEnabled = false;
            Save.IsEnabled = false;
            ReAuth.IsEnabled = true;
            File.IsEnabled = true;
            Projects.IsEnabled = false;

            _timeTaskService = new TimeTaskService(_userData.Token);

            Client.Text = _userData.Client;
            
            Projects.ItemsSource = new List<Project>
            {
                new Project
                {
                    name = _userData.Project,
                    id = _userData.ProjectId
                }
            };
            Projects.DisplayMemberPath = "name";
            Projects.SelectedIndex = 0;

            Modules.ItemsSource = new List<Projectmodule>
            {
                new Projectmodule
                {
                    modulename = _userData.Module,
                    moduleid = _userData.ModuleId
                }
            };
            Modules.DisplayMemberPath = "modulename";
            Modules.SelectedIndex = 0;

            WorkTypes.ItemsSource = new List<Projectworktype>
            {
                new Projectworktype
                {
                    worktype = _userData.WorkType,
                    worktypeid = _userData.WorkTypeId
                }
            };
            WorkTypes.DisplayMemberPath = "worktype";
            WorkTypes.SelectedIndex = 0;

        }

        private void Proceed()
        {
            UserToken.Text = string.Empty;
            UserToken.IsEnabled = false;
            Auth.IsEnabled = false;
            Client.Text = _primaryInformation.Client.client.name;

            Projects.ItemsSource = _primaryInformation.Projects.project;
            Projects.DisplayMemberPath = "name";
            Projects.IsEnabled = true;
            Modules.IsEnabled = true;
            WorkTypes.IsEnabled = true;

            File.IsEnabled = true;
        }

        private void File_Click(object sender, RoutedEventArgs e)
        {
            ReadFile();
        }

        private void ReadFile()
        {
            try
            {
                var fileDialog = new System.Windows.Forms.OpenFileDialog();
                var result = fileDialog.ShowDialog();
                switch (result)
                {
                    case System.Windows.Forms.DialogResult.OK:
                        if (fileDialog.FileName.Contains(".csv"))
                        {
                            if (int.TryParse(Day.Text, out var day) && int.TryParse(Hours.Text, out var hours))
                            {
                                var timeSet = ReadCSVFile.GetTimesPerDay(fileDialog.OpenFile(), day - 1, hours - 1);
                                var times = new List<TimeObject>();
                                var project = (Project) Projects.SelectedItem;
                                var module = (Projectmodule) Modules.SelectedItem;
                                var worktype = (Projectworktype) WorkTypes.SelectedItem;

                                times.AddRange(timeSet.Select(time => new TimeObject
                                { 
                                    personid = _primaryInformation  != null ? _primaryInformation.Me.personid.ToString() : _userData.PersonId,
                                    time = Math.Round(time.Value, 2).ToString(CultureInfo.InvariantCulture),
                                    billable = "t",
                                    date = DateTime.Parse(time.Key).ToString("yyyy-MM-dd"),
                                    projectid = int.Parse(project.id).ToString(),
                                    moduleid =  int.Parse(module.moduleid).ToString(),
                                    worktypeid = int.Parse(worktype.worktypeid).ToString()

                                }));
                                _userTimes = new UserTimes(_timeTaskService, times);
                                Submit.IsEnabled = true;
                            }
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Projects_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateModules();
            UpdateWorkTypes();
        }

        public async void UpdateModules()
        {
            if (Projects.SelectedItem != null && _primaryInformation != null)
            {
                var modules = await _primaryInformation.GetProjectModules((Project)Projects.SelectedItem);
                Modules.ItemsSource = modules.projectmodule;
                Modules.DisplayMemberPath = "modulename";
            }
        }

        public async void UpdateWorkTypes()
        {
            if (Projects.SelectedItem != null && _primaryInformation != null)
            {
                var worktypes = await _primaryInformation.GetProjectWorkTypes((Project)Projects.SelectedItem);
                WorkTypes.ItemsSource = worktypes.projectworktype;
                WorkTypes.DisplayMemberPath = "worktype";
            }   
        }

        public bool AllFieldsSelected()
        {
            return Projects.SelectedItem != null && WorkTypes.SelectedItem != null && Modules.SelectedItem != null;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (AllFieldsSelected())
            {
                try
                {
                    var project = (Project)Projects.SelectedItem;
                    var module = (Projectmodule)Modules.SelectedItem;
                    var workType = (Projectworktype)WorkTypes.SelectedItem;
                    SaveUserInformation.SaveUserInformationToCSV(
                        _primaryInformation.Me.personid.ToString(),
                        _timeTaskService._token,
                        _primaryInformation.Client.client.name,
                        _primaryInformation.Client.client.id,
                        project.name,
                        project.id,
                        module.modulename,
                        module.moduleid,
                        workType.worktype,
                        workType.worktypeid);
                    MessageBox.Show("Successfully saved user information");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    MessageBox.Show("Failed to save user information " + exception.InnerException);
                }
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            SubmitAllTimes();
        }

        private async void SubmitAllTimes()
        {
            var errors = await _userTimes.SubmitAllTimes();
            if (string.IsNullOrEmpty(errors))
            {
                MessageBox.Show("All times were submitted successfully");
            }
            else
            {
                MessageBox.Show("Failed to submit all times, Following day(s) Failed: " + errors);
            }
        }

        private void ReAuth_Click(object sender, RoutedEventArgs e)
        {
            UserToken.Text = string.Empty;
            Auth.IsEnabled = true;
            ReAuth.IsEnabled = false;
            Save.IsEnabled = true;
            Client.Text = string.Empty;
            Projects.ItemsSource = null;
            Projects.IsEnabled = false;
            Modules.ItemsSource = null;
            Modules.IsEnabled = false;
            WorkTypes.ItemsSource = null;
            WorkTypes.IsEnabled = false;
            File.IsEnabled = false;
            Submit.IsEnabled = false;
        }
    }
}
