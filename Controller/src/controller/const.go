package controller

var User string = "root"
//var Password string = "Yunshang2014"
var Password string = "root"
var SshPort string = "22"
var SdkFile string = "ys_service_static"
var IP string = "192.168.2.3"

var RemoteSdkPath string = "/test/sdk"
var RemoteSdk string = RemoteSdkPath +"/" + SdkFile

var LocalSdkPath string = "/root/sdk"
var LocalSdk string = LocalSdkPath + "/" + SdkFile

var PlayServer string = "192.168.2.7"
var PlayServerPort string = "9000"
var ControllerServer string = "192.168.100.200"

var SdkMachines = [...]string {
	"192.168.100.201",
	"192.168.100.202",
	"192.168.100.203",
	"192.168.100.204",
	"192.168.100.205",
}