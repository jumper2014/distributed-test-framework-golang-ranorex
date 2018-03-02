package controller

func StartSDK(ip string) {

	var command string = "chmod +x " + RemoteSdk
	RemoteExcuteCmd(ip, command)

	command = "nohup " + RemoteSdk + " > /dev/null 2>&1 &"
	RemoteExcuteCmd(ip, command)

}

func StopSDK(ip string) {
	//var command string = "killall -9 " + SdkFile
	var command string = "ps aux | grep ys_service_static |grep -v grep |awk -F ' ' '{print $2'} |xargs kill -9"
	RemoteExcuteCmd(ip, command)
}

func RemoveSDK(ip string) {
	var command string = "rm -rf " + RemoteSdkPath
	RemoteExcuteCmd(ip, command)

	command = "mkdir -p " + RemoteSdkPath
	RemoteExcuteCmd(ip, command)

	command = "chmod -R 777 " + RemoteSdkPath
	RemoteExcuteCmd(ip, command)
}

func DeploySDK(ip string, localSDK string, remotePath string) {
	ScpFileToRemote(ip, localSDK, remotePath)
}
