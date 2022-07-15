namespace MID_PLATFORM_CLIENT
{
    partial class LoginPopup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_user = new System.Windows.Forms.Label();
            this.textBox_User = new System.Windows.Forms.TextBox();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label_password = new System.Windows.Forms.Label();
            this.button_ShowPassword = new System.Windows.Forms.Button();
            this.button_Login = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_user
            // 
            this.label_user.AutoSize = true;
            this.label_user.Location = new System.Drawing.Point(12, 9);
            this.label_user.Name = "label_user";
            this.label_user.Size = new System.Drawing.Size(38, 20);
            this.label_user.TabIndex = 0;
            this.label_user.Text = "User";
            // 
            // textBox_User
            // 
            this.textBox_User.Location = new System.Drawing.Point(12, 32);
            this.textBox_User.Name = "textBox_User";
            this.textBox_User.Size = new System.Drawing.Size(243, 27);
            this.textBox_User.TabIndex = 1;
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(12, 104);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(243, 27);
            this.textBox_password.TabIndex = 3;
            this.textBox_password.UseSystemPasswordChar = true;
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(12, 81);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(70, 20);
            this.label_password.TabIndex = 2;
            this.label_password.Text = "Password";
            // 
            // button_ShowPassword
            // 
            this.button_ShowPassword.Location = new System.Drawing.Point(235, 78);
            this.button_ShowPassword.Name = "button_ShowPassword";
            this.button_ShowPassword.Size = new System.Drawing.Size(20, 20);
            this.button_ShowPassword.TabIndex = 4;
            this.button_ShowPassword.UseVisualStyleBackColor = true;
            this.button_ShowPassword.Click += new System.EventHandler(this.button_ShowPassword_Click);
            // 
            // button_Login
            // 
            this.button_Login.Location = new System.Drawing.Point(83, 157);
            this.button_Login.Name = "button_Login";
            this.button_Login.Size = new System.Drawing.Size(94, 29);
            this.button_Login.TabIndex = 5;
            this.button_Login.Text = "Login";
            this.button_Login.UseVisualStyleBackColor = true;
            this.button_Login.Click += new System.EventHandler(this.button_Login_Click);
            // 
            // LoginPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 198);
            this.Controls.Add(this.button_Login);
            this.Controls.Add(this.button_ShowPassword);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.textBox_User);
            this.Controls.Add(this.label_user);
            this.Name = "LoginPopup";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label_user;
        private TextBox textBox_User;
        private TextBox textBox_password;
        private Label label_password;
        private Button button_ShowPassword;
        private Button button_Login;
    }
}