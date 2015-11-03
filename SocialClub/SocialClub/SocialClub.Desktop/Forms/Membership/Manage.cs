// -----------------------------------------------------------------------
// <copyright file="Manage.cs" company="John">
//  Socia Member club Demo ©2013
// </copyright>
// -----------------------------------------------------------------------

namespace SocialClub.Desktop.Forms.Membership
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;
    using SocialClub.Data.Service;
    using SocialClub.Data.Entities;
    using SocialClub.Data.Enum;
    using SocialClub.Desktop;
    using SocialClub.Data.Service.Interface;
    using SocialClub.Desktop.Properties;
    using System.IO;

    /// <summary>
    /// Manage screen - To view, search, print, export club members information
    /// </summary>
    public partial class Manage : Form
    {
        /// <summary>
        /// Interface of ClubMemberService
        /// </summary>
        private IClubMemberService clubMemberService;

        /// <summary>
        /// Variable to store error message
        /// </summary>
        private string errorMessage;

        /// <summary>
        /// Member id
        /// </summary>
        private int memberId;
        
        /// <summary>
        /// Initializes a new instance of the Manage class
        /// </summary>
        public Manage()
        {
            this.InitializeComponent();
            this.InitializeResourceString();
            this.InitializeDropDownList();
            this.InitilizeDataGridViewStyle();
            this.clubMemberService = new ClubMemberService();            
            this.ResetRegistration();
            this.ResetSearch();
        }

        /// <summary>
        /// Initializes resource strings
        /// </summary>
        private void InitializeResourceString()
        {
            // Registeration
            lblName.Text = Resources.Registration_Name_Label_Text;
            lblDateOfBirth.Text = Resources.Registration_DateOfBirth_Label_Text;
            lblOccupation.Text = Resources.Registration_Occupation_Label_Text;
            lblMaritalStatus.Text = Resources.Registration_MaritalStatus_Label_Text;
            lblHealthStatus.Text = Resources.Registration_HealthStatus_Label_Text;
            lblSalary.Text = Resources.Registration_Salary_Label_Text;
            lblNoOfChildren.Text = Resources.Registration_Children_Label_Text;
            btnRegister.Text = Resources.Registration_Register_Button_Text;

            // Search, Print, Export, Update, Delete
            btnSearch.Text = Resources.Search_Search_Button_Text;
            btnRefresh.Text = Resources.Search_Refresh_Button_Text;
            btnUpdate.Text = Resources.Update_Button_Text;
            btnDelete.Text = Resources.Delete_Button_Text;
        }

        /// <summary>
        /// Initializes all dropdown controls
        /// </summary>
        private void InitializeDropDownList()
        {
            cmbOccupation.DataSource = Enum.GetValues(typeof(Occupation));
            cmbMaritalStatus.DataSource = Enum.GetValues(typeof(MaritalStatus));
            cmbHealthStatus.DataSource = Enum.GetValues(typeof(HealthStatus));            

            cmbSearchOccupation.DataSource = Enum.GetValues(typeof(Occupation));
            cmbSearchMaritalStatus.DataSource = Enum.GetValues(typeof(MaritalStatus));
            cmbOperand.SelectedIndex = 0;
        }

        /// <summary>
        /// Resets search criteria
        /// </summary>
        private void ResetSearch()
        {            
            cmbSearchMaritalStatus.SelectedIndex = -1;
            cmbSearchOccupation.SelectedIndex = -1;
            cmbOperand.SelectedIndex = 0;
        }

        /// <summary>
        /// Resets the registration screen
        /// </summary>
        private void ResetRegistration()
        {
            txtName.Text = string.Empty;
            txtSalary.Text = string.Empty;
            txtNoOfChildren.Text = string.Empty;
            cmbOccupation.SelectedIndex = -1;
            cmbHealthStatus.SelectedIndex = -1;
            cmbMaritalStatus.SelectedIndex = -1;
        }

        /// <summary>
        /// Initializes all dropdown controls in update section
        /// </summary>
        private void InitializeUpdate()
        {
            cmb2Occupation.DataSource = Enum.GetValues(typeof(Occupation));
            cmb2MaritalStatus.DataSource = Enum.GetValues(typeof(MaritalStatus));
            cmb2HealthStatus.DataSource = Enum.GetValues(typeof(HealthStatus));
        }

        /// <summary>
        /// Resets the update section of manage screen
        /// </summary>
        private void ResetUpdate()
        {
            txt2Name.Text = string.Empty;
            txt2Salary.Text = string.Empty;
            txt2NoOfChildren.Text = string.Empty;
            cmb2Occupation.SelectedIndex = -1;
            cmb2HealthStatus.SelectedIndex = -1;
            cmb2MaritalStatus.SelectedIndex = -1;
        }

        /// <summary>
        /// Validates registration input
        /// </summary>
        /// <returns>true or false</returns>
        private bool ValidateRegistration()
        {            
            this.errorMessage = string.Empty;

            if (txtName.Text.Trim() == string.Empty)
            {
                this.AddErrorMessage(Resources.Registration_Name_Required_Text);
            }

            if (cmbOccupation.SelectedIndex == -1)
            {
                this.AddErrorMessage(Resources.Registration_Occupation_Select_Text);
            }

            if (cmbMaritalStatus.SelectedIndex == -1)
            {
                this.AddErrorMessage(Resources.Registration_MaritalStatus_Select_Text);
            }

            if (cmbHealthStatus.SelectedIndex == -1)
            {
                this.AddErrorMessage(Resources.Registration_HealthStatus_Select_Text);
            }

            return this.errorMessage != string.Empty ? false : true;
        }

        /// <summary>
        /// Validates update data
        /// </summary>
        /// <returns>true or false</returns>
        private bool ValidateUpdate()
        {
            this.errorMessage = string.Empty;

            if (txt2Name.Text.Trim() == string.Empty)
            {
                this.AddErrorMessage(Resources.Registration_Name_Required_Text);
            }

            if (cmb2Occupation.SelectedIndex == -1)
            {
                this.AddErrorMessage(Resources.Registration_Occupation_Select_Text);
            }

            if (cmb2MaritalStatus.SelectedIndex == -1)
            {
                this.AddErrorMessage(Resources.Registration_MaritalStatus_Select_Text);
            }

            if (cmb2HealthStatus.SelectedIndex == -1)
            {
                this.AddErrorMessage(Resources.Registration_HealthStatus_Select_Text);
            }

            return this.errorMessage != string.Empty ? false : true;
        }

        /// <summary>
        /// To generate the error message
        /// </summary>
        /// <param name="error">error message</param>
        private void AddErrorMessage(string error)
        {
            if (this.errorMessage == string.Empty)
            {
                this.errorMessage = Resources.Error_Message_Header + "\n\n";
            }

            this.errorMessage += error + "\n";
        }

        /// <summary>
        /// Method to show general error message on any system level exception
        /// </summary>
        private void ShowErrorMessage(Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                //Resources.System_Error_Message, 
                Resources.System_Error_Message_Title, 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Error);
        }

        /// <summary>
        /// Initializes data grid view style
        /// </summary>
        private void InitilizeDataGridViewStyle()
        {
            // Setting the style of the DataGridView control
            dataGridViewMembers.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewMembers.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ControlDark;
            dataGridViewMembers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewMembers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewMembers.DefaultCellStyle.Font = new Font("Tahoma", 8, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewMembers.DefaultCellStyle.BackColor = Color.Empty;
            dataGridViewMembers.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.Info;
            dataGridViewMembers.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridViewMembers.GridColor = SystemColors.ControlDarkDark;
        }



        /// <summary>
        /// Method to load data grid view
        /// </summary>
        /// <param name="data">data table</param>
        private void LoadDataGridView(DataTable data)
        {
            // Data grid view column setting            
            dataGridViewMembers.DataSource = data;
            dataGridViewMembers.DataMember = data.TableName;
            dataGridViewMembers.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);            
        }

        /// <summary>
        /// Click event to handle registration
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event data</param>
        private void Register_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the validation passes
                if (this.ValidateRegistration())
                {
                    // Assign the values to the model
                    ClubMember clubMemberModel = new ClubMember()
                    {
                        Id = 0,
                        Name = txtName.Text.Trim(),
                        DateOfBirth = dtDateOfBirth.Value,
                        Occupation = (int)cmbOccupation.SelectedValue,
                        HealthStatus = (int)cmbHealthStatus.SelectedValue,
                        MaritalStatus = (int)cmbMaritalStatus.SelectedValue,
                        Salary = txtSalary.Text.Trim() == string.Empty ? 0 : Convert.ToDecimal(txtSalary.Text),
                        NumberOfChildren = txtNoOfChildren.Text.Trim() == string.Empty ? 0 : Convert.ToInt16(txtNoOfChildren.Text)
                    };

                    // Call the service method and assign the return status to variable
                    var success = this.clubMemberService.Create(clubMemberModel);

                    // if status of success variable is true then display a information else display the error message
                    if (success)
                    {
                        // display the message box
                        MessageBox.Show(
                            Resources.Registration_Successful_Message,
                            Resources.Registration_Successful_Message_Title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Reset the screen
                        this.ResetRegistration();
                    }
                    else
                    {
                        // display the error messge
                        MessageBox.Show(
                            Resources.Registration_Error_Message,
                            Resources.Registration_Error_Message_Title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Display the validation failed message
                    MessageBox.Show(
                        this.errorMessage, 
                        Resources.Registration_Error_Message_Title, 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// Key press Event to accept only numeric value
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">key press event data</param>
        private void Salary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Key press Event to accept only numeric value
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">event data</param>
        private void Children_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Event to handle tab selection
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event data</param>
        private void Tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tab.SelectedIndex == 1)
                {
                    DataTable data = this.clubMemberService.GetAll();
                    this.InitializeUpdate();
                    this.LoadDataGridView(data);                    
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// Event to handle the data formatting in data grid view
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event data</param>
        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    e.Value = string.Format("{0:dd/MM/yyyy}", (DateTime)e.Value);
                }

                if (e.ColumnIndex == 3)
                {
                    e.Value = Enum.GetName(typeof(Occupation), e.Value).ToString();
                }

                if (e.ColumnIndex == 5)
                {
                    e.Value = Enum.GetName(typeof(MaritalStatus), e.Value).ToString();
                }

                if (e.ColumnIndex == 6)
                {
                    e.Value = Enum.GetName(typeof(HealthStatus), e.Value).ToString();
                }

                if (e.ColumnIndex == 7)
                {
                    e.Value = Convert.ToDecimal(e.Value) == 0 ? string.Empty : e.Value;
                }

                if (e.ColumnIndex == 8)
                {
                    e.Value = Convert.ToInt16(e.Value) == 0 ? string.Empty : e.Value;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }            
        }     

        /// <summary>
        /// Click event to handle search
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event data</param>
        private void Search_Click(object sender, EventArgs e)
        {
            try
            {
                int occupation = cmbSearchOccupation.SelectedValue == null ? 
                    0 : (int)cmbSearchOccupation.SelectedValue;

                int maritalStatus = cmbSearchMaritalStatus.SelectedValue == null ?
                    0 : (int)cmbSearchMaritalStatus.SelectedValue;

                DataTable data = this.clubMemberService.Search(occupation, maritalStatus, cmbOperand.GetItemText(cmbOperand.SelectedItem));
                this.LoadDataGridView(data);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }            
        }       

        /// <summary>
        /// Click event to handle the refresh
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">event data</param>
        private void Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.ResetSearch();
                DataTable data = this.clubMemberService.GetAll();
                this.LoadDataGridView(data); 
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }            
        }

        private void dataGridViewMembers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentRow = dataGridViewMembers.SelectedCells[0].RowIndex;
            MessageBox.Show("cell content click");
            try
            {
                string clubMemberId = dataGridViewMembers[0, currentRow].Value.ToString();
                memberId = int.Parse(clubMemberId);
            }
            catch (Exception ex)
            {
                
            }
        }
       
        /// <summary>
        /// Click event to update the data
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event args</param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateUpdate())
                {
                    ClubMember clubMemberModel = new ClubMember()
                    {
                        Id = this.memberId,
                        Name = txt2Name.Text.Trim(),
                        DateOfBirth = dt2DateOfBirth.Value,
                        Occupation = (int)cmb2Occupation.SelectedValue,
                        HealthStatus = (int)cmb2HealthStatus.SelectedValue,
                        MaritalStatus = (int)cmb2MaritalStatus.SelectedValue,
                        Salary = txt2Salary.Text.Trim() == string.Empty ? 0 : Convert.ToDecimal(txt2Salary.Text),
                        NumberOfChildren = txt2NoOfChildren.Text.Trim() == string.Empty ? 0 : Convert.ToInt16(txt2NoOfChildren.Text)
                    };

                    var flag = this.clubMemberService.Update(clubMemberModel);

                    if (flag)
                    {
                        DataTable data = this.clubMemberService.GetAll();
                        this.LoadDataGridView(data);

                        MessageBox.Show(
                            Resources.Update_Successful_Message,
                            Resources.Update_Successful_Message_Title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);                        
                    }
                }
                else
                {
                    MessageBox.Show(
                        this.errorMessage,
                        Resources.Registration_Error_Message_Title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var flag = this.clubMemberService.Delete(this.memberId);

                if (flag)
                {
                    DataTable data = this.clubMemberService.GetAll();
                    this.LoadDataGridView(data);

                    MessageBox.Show(
                        Resources.Delete_Successful_Message,
                        Resources.Delete_Successful_Message_Title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                
            }
            catch (Exception ex)
            {                
                this.ShowErrorMessage(ex);
            }
        }

        private void dataGridViewMembers_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            try
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    string clubMemberId = dgv.SelectedRows[0].Cells[0].Value.ToString();
                    memberId = int.Parse(clubMemberId);

                    DataRow dataRow = this.clubMemberService.GetById(memberId);

                    txt2Name.Text = dataRow["Name"].ToString();
                    dt2DateOfBirth.Value = Convert.ToDateTime(dataRow["DateOfBirth"]);
                    cmb2Occupation.SelectedItem = (Occupation)dataRow["Occupation"];
                    cmb2MaritalStatus.SelectedItem = (MaritalStatus)dataRow["MaritalStatus"];
                    cmb2HealthStatus.SelectedItem = (HealthStatus)dataRow["HealthStatus"];
                    txt2Salary.Text = dataRow["Salary"].ToString() == "0.0000" ? string.Empty : dataRow["Salary"].ToString();
                    txt2NoOfChildren.Text = dataRow["NumberOfChildren"].ToString();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }            
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            DialogResult result = fd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {

                string file = fd.FileName;
                string fileName = Path.GetFileName(file);

                //get current logged in user MyDocuments folder
                string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                //MessageBox.Show(documentPath);

                File.Copy(file, documentPath + "\\" + fileName);

                MessageBox.Show("File uploaded successfully");
            }
        }
                         
    }
}
