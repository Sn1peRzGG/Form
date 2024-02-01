using System.Windows.Forms;
using System;
using System.Drawing;

namespace Login_and_Register_Form
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsername;  // Label для ім'я користувача або email
        private System.Windows.Forms.Label lblPassword;  // Label для пароля
        private System.Windows.Forms.Label lblEmail;  // Label для email
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnSwitch;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Text = "Login and Register Form";

            // Ініціалізація компонентів

            lblTitle = new System.Windows.Forms.Label();
            lblUsername = new System.Windows.Forms.Label();  // Label для ім'я користувача або email
            lblPassword = new System.Windows.Forms.Label();  // Label для пароля
            lblEmail = new System.Windows.Forms.Label();  // Label для email
            txtUsername = new System.Windows.Forms.TextBox();
            txtPassword = new System.Windows.Forms.TextBox();
            txtEmail = new System.Windows.Forms.TextBox();
            btnSubmit = new System.Windows.Forms.Button();
            btnSwitch = new System.Windows.Forms.Button();
            components = new System.ComponentModel.Container();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblTitle.Location = new System.Drawing.Point(45, 15);
            lblTitle.Size = new System.Drawing.Size(300, 30);
            lblTitle.Text = "Login";
            this.Controls.Add(lblTitle);

            // lblUsername
            lblUsername.AutoSize = true;
            lblUsername.Location = new System.Drawing.Point(50, 55);
            lblUsername.Size = new System.Drawing.Size(150, 20);
            lblUsername.Text = "Email or Username";
            this.Controls.Add(lblUsername);

            // lblEmail
            lblEmail.AutoSize = true;
            lblEmail.Location = new System.Drawing.Point(50, 105);
            lblEmail.Size = new System.Drawing.Size(150, 20);
            lblEmail.Text = "Email";
            lblEmail.Visible = false;
            this.Controls.Add(lblEmail);

            // lblPassword
            lblPassword.AutoSize = true;
            lblPassword.Location = new System.Drawing.Point(50, 105);
            lblPassword.Size = new System.Drawing.Size(150, 20);
            lblPassword.Text = "Password";
            this.Controls.Add(lblPassword);

            // txtUsername
            txtUsername.Location = new System.Drawing.Point(50, 70);
            txtUsername.Size = new System.Drawing.Size(300, 30);
            this.Controls.Add(txtUsername);

            // txtEmail
            txtEmail.Location = new System.Drawing.Point(50, 120);
            txtEmail.Size = new System.Drawing.Size(300, 30);
            txtEmail.Visible = false;
            this.Controls.Add(txtEmail);

            // txtPassword
            txtPassword.Location = new System.Drawing.Point(50, 120);
            txtPassword.Size = new System.Drawing.Size(300, 30);
            txtPassword.PasswordChar = '*';
            this.Controls.Add(txtPassword);

            // btnSubmit
            btnSubmit.Location = new System.Drawing.Point(50, 250);
            btnSubmit.Size = new System.Drawing.Size(140, 40);
            btnSubmit.Text = "Login";
            btnSubmit.Click += new System.EventHandler(btnSubmit_Click);
            this.Controls.Add(btnSubmit);

            // btnSwitch
            btnSwitch.Location = new System.Drawing.Point(210, 250);
            btnSwitch.Size = new System.Drawing.Size(140, 40);
            btnSwitch.Text = "Switch to Register";
            btnSwitch.Click += new System.EventHandler(btnSwitch_Click);
            this.Controls.Add(btnSwitch);

            // btnSubmit
            btnSubmit.MouseEnter += new System.EventHandler(btnSubmitHover);
            btnSubmit.MouseLeave += new System.EventHandler(btnSubmitNotHover);
            this.Controls.Add(btnSubmit);

            // btnSwitch
            btnSwitch.MouseEnter += new System.EventHandler(btnSwitchHover);
            btnSwitch.MouseLeave += new System.EventHandler(btnSwitchNotHover);
            this.Controls.Add(btnSwitch);
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (btnSwitch.Text == "Switch to Register")
            {
                lblTitle.Text = "Registration";
                btnSwitch.Text = "Switch to Login";
                btnSubmit.Text = "Register";
                lblUsername.Text = "Username";
                lblPassword.Location = new System.Drawing.Point(50, 160);
                txtPassword.Location = new System.Drawing.Point(50, 175);
                lblEmail.Visible = true;  // Показати Label для Email при реєстрації
                txtEmail.Visible = true;  // Показати поле для Email при реєстрації
            }
            else
            {
                lblTitle.Text = "Login";
                btnSwitch.Text = "Switch to Register";
                btnSubmit.Text = "Login";
                lblPassword.Location = new System.Drawing.Point(50, 105);
                txtPassword.Location = new System.Drawing.Point(50, 120);
                lblEmail.Visible = false;  // Приховати Label для Email при вході
                txtEmail.Visible = false;  // Приховати поле для Email при вході
            }

            ClearFields();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSwitch.Text == "Switch to Register")
            {
                string usernameOrEmail = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Перевірка на пустоту полів
                if (string.IsNullOrEmpty(usernameOrEmail) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool loginSuccessful = DBHelper.Login(usernameOrEmail, password);

                if (loginSuccessful)
                {
                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();
                string email = txtEmail.Text.Trim();

                // Перевірка на пустоту полів
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool registrationSuccessful = DBHelper.Register(username, password, email);

                if (registrationSuccessful)
                {
                    MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Registration failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            ClearFields();
        }



        private void btnSubmitHover(object sender, EventArgs e)
        {
            btnSubmit.BackColor = Color.LightGray;
        }

        private void btnSubmitNotHover(object sender, EventArgs e)
        {
            btnSubmit.BackColor = SystemColors.Control;
        }

        private void btnSwitchHover(object sender, EventArgs e)
        {
            btnSwitch.BackColor = Color.LightGray;
        }

        private void btnSwitchNotHover(object sender, EventArgs e)
        {
            btnSwitch.BackColor = SystemColors.Control;
        }

        // ...

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
