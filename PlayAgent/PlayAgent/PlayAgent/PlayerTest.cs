/*
 * Created by Ranorex
 * User: zengyuetian
 * Date: 2017/8/10
 * Time: 16:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;
using System.Collections;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace PlayAgent
{
	
	public class Commander {
		public static string InvokeCmd(string cmdArgs)
		{
			string Tstr = "";
			Process p = new Process();
			p.StartInfo.FileName = "cmd.exe";
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardError = true;
			p.StartInfo.CreateNoWindow = true;
			p.Start();
			
			p.StandardInput.WriteLine(cmdArgs);
			p.StandardInput.WriteLine("exit");
			Tstr = p.StandardOutput.ReadToEnd();
			p.Close();
			return Tstr;
		}
	}
	/// <summary>
	/// Description of UserCodeModule1.
	/// </summary>
	[TestModule("CFF1CF98-B163-4396-8DEE-F7CB3DDDBE5C", ModuleType.UserCode, 1)]
	public class PlayerTest : ITestModule
	{
		
		public static PlayAgentRepository repo = PlayAgentRepository.Instance;
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public PlayerTest()
		{
			// Do not delete - a parameterless constructor is required!
		}
		
		

		/// <summary>
		/// Performs the playback of actions in this module.
		/// </summary>
		/// <remarks>You should not call this method directly, instead pass the module
		/// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
		/// that will in turn invoke this method.</remarks>
		void ITestModule.Run()
		{
			
			// settings for speed
			Mouse.DefaultMoveTime = 500;
			Keyboard.DefaultKeyPressTime = 50;
			Delay.SpeedFactor = 1.0;
			
			String PlayerUrl = "http://localhost:9000/public/player/live_test.swf";
			int sdkNum = 5;		// How many sdk start
			
			// kill previous IE processes
			PlayAgent.Commander.InvokeCmd("taskkill /f /IM iexplore.exe");
			Report.Log(ReportLevel.Info, "Close IE ");
			
			Delay.Milliseconds(1000);
			Process.Start("iexplore.exe");
			Report.Log(ReportLevel.Info, "Start IE");
			
//			repo.BlankPage.Self.Click();
			int port = 32717;
			
			ArrayList ipList=new ArrayList();
			ipList.Add("192.168.100.201");
			ipList.Add("192.168.100.202");
			ipList.Add("192.168.100.203");
			ipList.Add("192.168.100.204");
			ipList.Add("192.168.100.205");
			
			for(int i=0; i<sdkNum; i++) {
				if ( i!=0 ) {   					// open a new tab
					Keyboard.Press(System.Windows.Forms.Keys.T | System.Windows.Forms.Keys.Control, 20, Keyboard.DefaultKeyPressTime, 1, true);
				}
				
				String url = "http://"+ipList[i]+":" + port.ToString() + "/live_flv/user/xmtv?url=http://pl8.live.panda.tv/live_panda/012192e6277f7da070480d8ee32648fd.flv";
				repo.HttpLocalhost9000PublicPlayerLive.Edit.Click();
				Keyboard.Press(System.Windows.Forms.Keys.A | System.Windows.Forms.Keys.Control, 30, Keyboard.DefaultKeyPressTime, 1, true);
				repo.HttpLocalhost9000PublicPlayerLive.Edit.PressKeys("{Delete}");
				repo.HttpLocalhost9000PublicPlayerLive.Edit.PressKeys(PlayerUrl);
				Keyboard.Press(System.Windows.Forms.Keys.Enter);
				Report.Log(ReportLevel.Info, "Enter Flash Player URL");
				
				
				Delay.Milliseconds(1000);
				repo.WwwCutvCom.HttpLocalhost9000PublicPlayerLive.RtmpTxt.Click();
				Keyboard.Press(System.Windows.Forms.Keys.A | System.Windows.Forms.Keys.Control, 30, Keyboard.DefaultKeyPressTime, 1, true);				
				repo.WwwCutvCom.HttpLocalhost9000PublicPlayerLive.RtmpTxt.PressKeys("{Delete}");
				repo.WwwCutvCom.HttpLocalhost9000PublicPlayerLive.RtmpTxt.PressKeys(url);
				repo.WwwCutvCom.HttpLocalhost9000PublicPlayerLive.播放N.Click();
				Report.Log(ReportLevel.Info, "Enter Channel URL for " + ipList[i]);
				Delay.Milliseconds(3000);
				
			}
			
			
			while(true) {
				// select tabs one by one
				for(int i=0; i<=sdkNum-1; i++) {
//					TabPage tabPage = string.Format("/form[@title~'^http://localhost/live_tes']/element[@class='WorkerW']//tabpagelist[@accessiblename='选项卡行']/tabpage[@accessiblerole='PageTab' and @accessibledescription~'^localhosthttp://localho' and @title='localhost' and @index='{0}']", i);
					TabPage tabPage = string.Format("/form[@title~'^http://localhost:9000/pub']/element[@class='WorkerW']//tabpagelist[@accessiblename='选项卡行']/tabpage[@accessiblerole='PageTab' and @accessibledescription~'^localhosthttp://localho' and @title='localhost' and @index='{0}']", i);
					tabPage.Click();
					
					String text = repo.WwwCutvCom.HttpLocalhost9000PublicPlayerLive.StartAndBuffer.Caption;
					Report.Log(ReportLevel.Info, text);

					String newText = text.Replace("\\n", " ");
					Console.Out.Write(newText);
					
					string[] sArray=Regex.Split(newText," ",RegexOptions.IgnoreCase);
					Report.Log(ReportLevel.Info,"start time:"+sArray[0]);
					Report.Log(ReportLevel.Info,"buffer number:"+sArray[1]);
					
					string status = "StartTime:"+sArray[0]+"-"+"BufferNumber:"+sArray[1];
					PlayAgent.Commander.InvokeCmd("echo "+status + " > C:/cybertron/Controller/src/playServer/public/player/" + string.Format("{0}.html", ipList[i]));
					Report.Log(ReportLevel.Info, "Get Data for " + ipList[i]);
					Delay.Milliseconds(5000);
					
					
				}
				Delay.Milliseconds(60000);
			}
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
		}
	}
}
