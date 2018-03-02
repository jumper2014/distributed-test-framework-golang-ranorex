package main

import (
	"fmt"
	"controller"
	"encoding/json"
	"time"
)

func main() {

	fmt.Println("Start to collect result")

	for i:=0 ; i<len(controller.SdkMachines); i++ {
		ip := controller.SdkMachines[i]
		fmt.Println("--------------------------------")
		fmt.Println("Collect result for machine:"+ip)
		time.Sleep(time.Second*1)

		url := "http://"+ip+":32717/ajax/report"
		result := controller.HttpGet(url)

		//json str 转map
		var dat map[string]interface{}
		if err := json.Unmarshal([]byte(result), &dat); err == nil {
			fmt.Println("Download rate(k) is:")
			fmt.Println(dat["download_rate"])
		}

		// 收集卡顿数据
		url = "http://"+controller.PlayServer+":"+controller.PlayServerPort+"/public/player/"+ip+".html"
		result = controller.HttpGet(url)
		fmt.Println("BufferQuantity:" +result)
	}

}

