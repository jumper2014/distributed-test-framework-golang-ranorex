// go run main.go -start_sdk	-> start sdk
// go run main.go -stop_sdk   	-> stop
// go run main.go -start_play  	-> start player
// go run main.go -stop_play    -> stop player

package main

import (
	"fmt"
	"controller"
	"time"
	"flag"

)

func main() {
	start_sdk := flag.Bool("start_sdk", false, "start sdk")
	stop_sdk := flag.Bool("stop_sdk", false, "stop sdk")
	start_play := flag.Bool("start_play", false, "start play")
	stop_play := flag.Bool("stop_play", false, "stop play")
	flag.Parse()

	if !*start_sdk && !*stop_sdk && !*start_play && !*stop_play {
		fmt.Println("go run main.go [-start_sdk | -stop_sdk | -start_play | -stop_play ]")
	}


	for i:=0 ; i<len(controller.SdkMachines); i++ {
		sdk_ip := controller.SdkMachines[i]
		if !*start_play && !*stop_play {

			if *start_sdk || *stop_sdk {

				fmt.Println("--------------------------------")
				fmt.Println("Control for machine:" + sdk_ip)
				time.Sleep(time.Second * 1)

				fmt.Println("Stop SDK")
				controller.StopSDK(sdk_ip)
			}

			if *start_sdk {
				fmt.Println("Remove SDK")
				controller.RemoveSDK(sdk_ip)

				time.Sleep(time.Second * 1)
				fmt.Println("Deploy SDK")
				controller.DeploySDK(sdk_ip, controller.LocalSdk, controller.RemoteSdkPath)

				fmt.Println("Start SDK")
				controller.StartSDK(sdk_ip)
			}
		}


	}
	if *start_play {
		url := "http://"+controller.PlayServer+":"+controller.PlayServerPort+"/start"
		controller.HttpGet(url)
	}
	if *stop_play {
		url := "http://"+controller.PlayServer+":"+controller.PlayServerPort+"/stop"
		controller.HttpGet(url)
	}

}

