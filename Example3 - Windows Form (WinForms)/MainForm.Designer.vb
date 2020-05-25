<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.txtInput = New System.Windows.Forms.TextBox()
        Me.lblInput = New System.Windows.Forms.Label()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.lblUptime = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.grpMain = New System.Windows.Forms.GroupBox()
        Me.ControlCenter = New System.Windows.Forms.TabControl()
        Me.tabDiscord = New System.Windows.Forms.TabPage()
        Me.grpChat = New System.Windows.Forms.GroupBox()
        Me.lstChat = New System.Windows.Forms.ListBox()
        Me.grpChannels = New System.Windows.Forms.GroupBox()
        Me.lstChannels = New System.Windows.Forms.ListBox()
        Me.grpGuilds = New System.Windows.Forms.GroupBox()
        Me.lstGuilds = New System.Windows.Forms.ListBox()
        Me.tabConsole = New System.Windows.Forms.TabPage()
        Me.lstConsole = New System.Windows.Forms.ListBox()
        Me.lblGuildCount = New System.Windows.Forms.Label()
        Me.picProfile = New System.Windows.Forms.PictureBox()
        Me.grpGuildCount = New System.Windows.Forms.GroupBox()
        Me.lblUserCount = New System.Windows.Forms.Label()
        Me.grpUserCount = New System.Windows.Forms.GroupBox()
        Me.lblMessageCount = New System.Windows.Forms.Label()
        Me.grpMessageCount = New System.Windows.Forms.GroupBox()
        Me.NotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ctxIconMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmLogout = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.grpMain.SuspendLayout()
        Me.ControlCenter.SuspendLayout()
        Me.tabDiscord.SuspendLayout()
        Me.grpChat.SuspendLayout()
        Me.grpChannels.SuspendLayout()
        Me.grpGuilds.SuspendLayout()
        Me.tabConsole.SuspendLayout()
        CType(Me.picProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGuildCount.SuspendLayout()
        Me.grpUserCount.SuspendLayout()
        Me.grpMessageCount.SuspendLayout()
        Me.ctxIconMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtInput
        '
        Me.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInput.Location = New System.Drawing.Point(643, 618)
        Me.txtInput.Name = "txtInput"
        Me.txtInput.Size = New System.Drawing.Size(477, 31)
        Me.txtInput.TabIndex = 1
        Me.txtInput.UseSystemPasswordChar = True
        '
        'lblInput
        '
        Me.lblInput.AutoSize = True
        Me.lblInput.Location = New System.Drawing.Point(572, 623)
        Me.lblInput.Name = "lblInput"
        Me.lblInput.Size = New System.Drawing.Size(62, 25)
        Me.lblInput.TabIndex = 0
        Me.lblInput.Text = "Token:"
        '
        'btnSubmit
        '
        Me.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSubmit.Location = New System.Drawing.Point(1126, 617)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(110, 40)
        Me.btnSubmit.TabIndex = 2
        Me.btnSubmit.Text = "Start Bot"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'lblUptime
        '
        Me.lblUptime.AutoSize = True
        Me.lblUptime.Location = New System.Drawing.Point(12, 630)
        Me.lblUptime.Name = "lblUptime"
        Me.lblUptime.Size = New System.Drawing.Size(147, 25)
        Me.lblUptime.TabIndex = 0
        Me.lblUptime.Text = "Uptime: 00:00:00"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(1109, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(119, 25)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "Disconnected"
        '
        'grpMain
        '
        Me.grpMain.Controls.Add(Me.lblStatus)
        Me.grpMain.Controls.Add(Me.ControlCenter)
        Me.grpMain.Location = New System.Drawing.Point(12, 165)
        Me.grpMain.Name = "grpMain"
        Me.grpMain.Size = New System.Drawing.Size(1234, 448)
        Me.grpMain.TabIndex = 0
        Me.grpMain.TabStop = False
        '
        'ControlCenter
        '
        Me.ControlCenter.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.ControlCenter.Controls.Add(Me.tabDiscord)
        Me.ControlCenter.Controls.Add(Me.tabConsole)
        Me.ControlCenter.Location = New System.Drawing.Point(6, 17)
        Me.ControlCenter.Name = "ControlCenter"
        Me.ControlCenter.SelectedIndex = 0
        Me.ControlCenter.Size = New System.Drawing.Size(1222, 425)
        Me.ControlCenter.TabIndex = 5
        '
        'tabDiscord
        '
        Me.tabDiscord.Controls.Add(Me.grpChat)
        Me.tabDiscord.Controls.Add(Me.grpChannels)
        Me.tabDiscord.Controls.Add(Me.grpGuilds)
        Me.tabDiscord.Location = New System.Drawing.Point(4, 37)
        Me.tabDiscord.Name = "tabDiscord"
        Me.tabDiscord.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDiscord.Size = New System.Drawing.Size(1214, 384)
        Me.tabDiscord.TabIndex = 0
        Me.tabDiscord.Text = "Discord"
        Me.tabDiscord.UseVisualStyleBackColor = True
        '
        'grpChat
        '
        Me.grpChat.Controls.Add(Me.lstChat)
        Me.grpChat.Location = New System.Drawing.Point(621, 3)
        Me.grpChat.Name = "grpChat"
        Me.grpChat.Size = New System.Drawing.Size(586, 381)
        Me.grpChat.TabIndex = 5
        Me.grpChat.TabStop = False
        Me.grpChat.Text = "Chat"
        '
        'lstChat
        '
        Me.lstChat.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstChat.FormattingEnabled = True
        Me.lstChat.ItemHeight = 25
        Me.lstChat.Location = New System.Drawing.Point(6, 24)
        Me.lstChat.Name = "lstChat"
        Me.lstChat.Size = New System.Drawing.Size(574, 350)
        Me.lstChat.TabIndex = 5
        '
        'grpChannels
        '
        Me.grpChannels.Controls.Add(Me.lstChannels)
        Me.grpChannels.Location = New System.Drawing.Point(312, 3)
        Me.grpChannels.Name = "grpChannels"
        Me.grpChannels.Size = New System.Drawing.Size(300, 381)
        Me.grpChannels.TabIndex = 5
        Me.grpChannels.TabStop = False
        Me.grpChannels.Text = "Channels"
        '
        'lstChannels
        '
        Me.lstChannels.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstChannels.FormattingEnabled = True
        Me.lstChannels.ItemHeight = 25
        Me.lstChannels.Location = New System.Drawing.Point(6, 24)
        Me.lstChannels.Name = "lstChannels"
        Me.lstChannels.Size = New System.Drawing.Size(288, 350)
        Me.lstChannels.TabIndex = 5
        '
        'grpGuilds
        '
        Me.grpGuilds.Controls.Add(Me.lstGuilds)
        Me.grpGuilds.Location = New System.Drawing.Point(3, 3)
        Me.grpGuilds.Name = "grpGuilds"
        Me.grpGuilds.Size = New System.Drawing.Size(300, 381)
        Me.grpGuilds.TabIndex = 5
        Me.grpGuilds.TabStop = False
        Me.grpGuilds.Text = "Guilds"
        '
        'lstGuilds
        '
        Me.lstGuilds.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstGuilds.FormattingEnabled = True
        Me.lstGuilds.ItemHeight = 25
        Me.lstGuilds.Location = New System.Drawing.Point(6, 24)
        Me.lstGuilds.Name = "lstGuilds"
        Me.lstGuilds.Size = New System.Drawing.Size(288, 350)
        Me.lstGuilds.TabIndex = 5
        '
        'tabConsole
        '
        Me.tabConsole.Controls.Add(Me.lstConsole)
        Me.tabConsole.Location = New System.Drawing.Point(4, 37)
        Me.tabConsole.Name = "tabConsole"
        Me.tabConsole.Padding = New System.Windows.Forms.Padding(3)
        Me.tabConsole.Size = New System.Drawing.Size(1214, 384)
        Me.tabConsole.TabIndex = 1
        Me.tabConsole.Text = "Console"
        Me.tabConsole.UseVisualStyleBackColor = True
        '
        'lstConsole
        '
        Me.lstConsole.BackColor = System.Drawing.SystemColors.Desktop
        Me.lstConsole.Font = New System.Drawing.Font("Consolas", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lstConsole.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lstConsole.FormattingEnabled = True
        Me.lstConsole.ItemHeight = 23
        Me.lstConsole.Location = New System.Drawing.Point(0, 0)
        Me.lstConsole.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.lstConsole.Name = "lstConsole"
        Me.lstConsole.Size = New System.Drawing.Size(1211, 372)
        Me.lstConsole.TabIndex = 0
        '
        'lblGuildCount
        '
        Me.lblGuildCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblGuildCount.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblGuildCount.Location = New System.Drawing.Point(20, 22)
        Me.lblGuildCount.Name = "lblGuildCount"
        Me.lblGuildCount.Size = New System.Drawing.Size(250, 125)
        Me.lblGuildCount.TabIndex = 0
        Me.lblGuildCount.Text = "Guild Count"
        Me.lblGuildCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'picProfile
        '
        Me.picProfile.ErrorImage = Nothing
        Me.picProfile.Image = CType(resources.GetObject("picProfile.Image"), System.Drawing.Image)
        Me.picProfile.InitialImage = CType(resources.GetObject("picProfile.InitialImage"), System.Drawing.Image)
        Me.picProfile.Location = New System.Drawing.Point(1096, 9)
        Me.picProfile.Name = "picProfile"
        Me.picProfile.Size = New System.Drawing.Size(150, 150)
        Me.picProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picProfile.TabIndex = 4
        Me.picProfile.TabStop = False
        '
        'grpGuildCount
        '
        Me.grpGuildCount.Controls.Add(Me.lblGuildCount)
        Me.grpGuildCount.Location = New System.Drawing.Point(12, 9)
        Me.grpGuildCount.Name = "grpGuildCount"
        Me.grpGuildCount.Size = New System.Drawing.Size(300, 155)
        Me.grpGuildCount.TabIndex = 0
        Me.grpGuildCount.TabStop = False
        Me.grpGuildCount.Text = "Guild Count"
        '
        'lblUserCount
        '
        Me.lblUserCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblUserCount.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblUserCount.Location = New System.Drawing.Point(20, 22)
        Me.lblUserCount.Name = "lblUserCount"
        Me.lblUserCount.Size = New System.Drawing.Size(250, 125)
        Me.lblUserCount.TabIndex = 0
        Me.lblUserCount.Text = "User Count"
        Me.lblUserCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpUserCount
        '
        Me.grpUserCount.Controls.Add(Me.lblUserCount)
        Me.grpUserCount.Location = New System.Drawing.Point(365, 9)
        Me.grpUserCount.Name = "grpUserCount"
        Me.grpUserCount.Size = New System.Drawing.Size(300, 155)
        Me.grpUserCount.TabIndex = 0
        Me.grpUserCount.TabStop = False
        Me.grpUserCount.Text = "User Count"
        '
        'lblMessageCount
        '
        Me.lblMessageCount.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblMessageCount.Location = New System.Drawing.Point(20, 24)
        Me.lblMessageCount.Margin = New System.Windows.Forms.Padding(0)
        Me.lblMessageCount.Name = "lblMessageCount"
        Me.lblMessageCount.Size = New System.Drawing.Size(250, 125)
        Me.lblMessageCount.TabIndex = 0
        Me.lblMessageCount.Text = "Message Count"
        Me.lblMessageCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpMessageCount
        '
        Me.grpMessageCount.Controls.Add(Me.lblMessageCount)
        Me.grpMessageCount.Location = New System.Drawing.Point(728, 9)
        Me.grpMessageCount.Name = "grpMessageCount"
        Me.grpMessageCount.Size = New System.Drawing.Size(300, 155)
        Me.grpMessageCount.TabIndex = 0
        Me.grpMessageCount.TabStop = False
        Me.grpMessageCount.Text = "Message Count"
        '
        'NotifyIcon
        '
        Me.NotifyIcon.ContextMenuStrip = Me.ctxIconMenu
        Me.NotifyIcon.Icon = CType(resources.GetObject("NotifyIcon.Icon"), System.Drawing.Icon)
        Me.NotifyIcon.Text = "Disqord"
        Me.NotifyIcon.Visible = True
        '
        'ctxIconMenu
        '
        Me.ctxIconMenu.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ctxIconMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmLogout, Me.ToolStripSeparator1, Me.tsmExit})
        Me.ctxIconMenu.Name = "ctxIconMenu"
        Me.ctxIconMenu.Size = New System.Drawing.Size(142, 74)
        '
        'tsmLogout
        '
        Me.tsmLogout.Name = "tsmLogout"
        Me.tsmLogout.Size = New System.Drawing.Size(141, 32)
        Me.tsmLogout.Text = "Logout"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(138, 6)
        '
        'tsmExit
        '
        Me.tsmExit.Name = "tsmExit"
        Me.tsmExit.Size = New System.Drawing.Size(141, 32)
        Me.tsmExit.Text = "Exit"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1258, 669)
        Me.Controls.Add(Me.grpMain)
        Me.Controls.Add(Me.grpMessageCount)
        Me.Controls.Add(Me.grpUserCount)
        Me.Controls.Add(Me.picProfile)
        Me.Controls.Add(Me.lblUptime)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.lblInput)
        Me.Controls.Add(Me.txtInput)
        Me.Controls.Add(Me.grpGuildCount)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.Text = "Disqord Winforms Example"
        Me.grpMain.ResumeLayout(False)
        Me.grpMain.PerformLayout()
        Me.ControlCenter.ResumeLayout(False)
        Me.tabDiscord.ResumeLayout(False)
        Me.grpChat.ResumeLayout(False)
        Me.grpChannels.ResumeLayout(False)
        Me.grpGuilds.ResumeLayout(False)
        Me.tabConsole.ResumeLayout(False)
        CType(Me.picProfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGuildCount.ResumeLayout(False)
        Me.grpUserCount.ResumeLayout(False)
        Me.grpMessageCount.ResumeLayout(False)
        Me.ctxIconMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtInput As Windows.Forms.TextBox
    Friend WithEvents lblInput As Windows.Forms.Label
    Friend WithEvents btnSubmit As Windows.Forms.Button
    Friend WithEvents lblUptime As Windows.Forms.Label
    Friend WithEvents lblStatus As Windows.Forms.Label
    Friend WithEvents grpMain As Windows.Forms.GroupBox
    Friend WithEvents lblGuildCount As Windows.Forms.Label
    Friend WithEvents picProfile As Windows.Forms.PictureBox
    Friend WithEvents grpGuilds As Windows.Forms.GroupBox
    Friend WithEvents lblUserCount As Windows.Forms.Label
    Friend WithEvents grpUsers As Windows.Forms.GroupBox
    Friend WithEvents lblMessageCount As Windows.Forms.Label
    Friend WithEvents grpMessages As Windows.Forms.GroupBox
    Friend WithEvents ControlCenter As Windows.Forms.TabControl
    Friend WithEvents tabDiscord As Windows.Forms.TabPage
    Friend WithEvents tabConsole As Windows.Forms.TabPage
    Friend WithEvents lstConsole As Windows.Forms.ListBox
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents grpGuildCount As Windows.Forms.GroupBox
    Friend WithEvents grpUserCount As Windows.Forms.GroupBox
    Friend WithEvents grpMessageCount As Windows.Forms.GroupBox
    Friend WithEvents grpChannels As Windows.Forms.GroupBox
    Friend WithEvents grpChat As Windows.Forms.GroupBox
    Friend WithEvents grpDiscord As Windows.Forms.GroupBox
    Friend WithEvents grpGuild As Windows.Forms.GroupBox
    Friend WithEvents lstGuilds As Windows.Forms.ListBox
    Friend WithEvents lstChat As Windows.Forms.ListBox
    Friend WithEvents lstChannels As Windows.Forms.ListBox
    Friend WithEvents NotifyIcon As Windows.Forms.NotifyIcon
    Friend WithEvents ctxIconMenu As Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmLogout As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmExit As Windows.Forms.ToolStripMenuItem
End Class
