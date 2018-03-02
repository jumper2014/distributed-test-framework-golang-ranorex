package controller

import (
	"net/http"
	"io/ioutil"
	"fmt"
	"strings"
)

func HttpGet(url string) string {
	resp, err := http.Get(url)
	if err != nil {
		// handle error
	}

	defer resp.Body.Close()
	body, err := ioutil.ReadAll(resp.Body)
	if err != nil {
		// handle error
	}

	result := string(body)
	//fmt.Println(result)
	return result
}


func HttpPost(url string) string {
	resp, err := http.Post(url,
		"application/json",
		strings.NewReader("name=cjb"))
	if err != nil {
		fmt.Println(err)
	}

	defer resp.Body.Close()
	body, err := ioutil.ReadAll(resp.Body)
	if err != nil {
		// handle error
	}

	result := string(body)
	fmt.Println(result)
	return result
}
