package main

import (
	"flag"
	"fmt"
)

func main() {
	ok := flag.Bool("ok", false, "is ok")
	//id := flag.Int("id", 0, "id")
	//port := flag.String("port", ":8080", "http listen port")
	//var name string
	//flag.StringVar(&name, "name", "123", "name")

	flag.Parse()

	fmt.Println("ok:", *ok)
	//fmt.Println("id:", *id)
	//fmt.Println("port:", *port)
	//fmt.Println("name:", name)
}
