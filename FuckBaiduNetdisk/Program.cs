using Microsoft.Win32;

namespace FuckBaiduNetdisk
{
    //<requestedExecutionLevel level = "requireAdministrator" uiAccess="false" />
    internal static class Program
    {
        const string BaiduKeyName = ".WorkspaceExt";

        [STAThread]
        static void Main()
        {
            //.WorkspaceExt0]
            //.WorkspaceExt1]
            //.WorkspaceExt2]
            //.WorkspaceExt3]
            //.WorkspaceExt4]
            try
            {
                /**
                 * ��ǰ�û��ǹ���Ա��ʱ��ֱ������Ӧ�ó���
                 * ������ǹ���Ա����ʹ��������������������ȷ��ʹ�ù���Ա�������
                 */
                //��õ�ǰ��¼��Windows�û���ʾ
                System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                //�жϵ�ǰ��¼�û��Ƿ�Ϊ����Ա
                if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                {
                    //����ǹ���Ա����ֱ������
                    DeleteKey();
                }
                else
                {
                    //������������
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.UseShellExecute = true;
                    startInfo.WorkingDirectory = Environment.CurrentDirectory;
                    startInfo.FileName = Application.ExecutablePath;
                    //������������,ȷ���Թ���Ա�������
                    startInfo.Verb = "runas";
                    try
                    {
                        System.Diagnostics.Process.Start(startInfo);
                    }
                    catch
                    {
                        return;
                    }
                    //�˳�
                    Application.Exit();
                }

            }
            catch (Exception err)
            {
                Console.Error.WriteLine(err.Message);
            }
        }

        static void DeleteKey()
        {
            //�����\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ShellIconOverlayIdentifiers
            var key = Registry.LocalMachine;
            var shellIconOverlayIdentifiers = key
                .OpenSubKey("SOFTWARE", true)
                .OpenSubKey("Microsoft", true)
                .OpenSubKey("Windows", true)
                .OpenSubKey("CurrentVersion", true)
                .OpenSubKey("Explorer", true)
                .OpenSubKey("ShellIconOverlayIdentifiers", true);
            var subkeyNames = shellIconOverlayIdentifiers.GetSubKeyNames();
            foreach (string keyName in subkeyNames)
            {
                if (keyName.Trim().ToLower().StartsWith(BaiduKeyName.ToLower()))
                {
                    shellIconOverlayIdentifiers.DeleteSubKey(keyName, true);
                    Console.WriteLine($"Delete [{keyName}]");
                }
            }
            key.Close();
        }
    }
}