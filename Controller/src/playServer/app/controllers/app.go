package controllers

import (
	"github.com/revel/revel"
	"os/exec"
	"bytes"
	"log"
)

type App struct {
	*revel.Controller
}

type Html string


func (c App) Index() revel.Result {
	return c.Render()
}

func (c App) Start() revel.Result {
	cmd := exec.Command("cmd", "/c C:\\cybertron\\PlayAgent\\PlayAgent\\startRanorex.cmd")
	var out bytes.Buffer
	cmd.Stdout = &out
	err := cmd.Run()
	if err != nil {
		log.Fatal(err)
	}
	return c.Render()

}

func (c App) Stop() revel.Result {
	cmd := exec.Command("cmd", "/c taskkill /f /IM PlayAgent.exe")
	err := cmd.Start()
	if err != nil {
		log.Fatal(err)
	}
	log.Printf("Waiting for command to finish...")
	err = cmd.Wait()
	return c.Render()
}
