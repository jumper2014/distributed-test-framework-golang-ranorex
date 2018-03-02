# Yunshang Stability Test Framework

## 3 main roles
    Controller machine      - Linux
    Player machine          - Windows + flash debugger player
    SDK machines            - Linux
    
Controller machine will stop player, stop sdk ,deploy sdk, start sdk
Player machine run web service for swf player, accept HTTP request to start Ranorex to control player

## Implementation
### Controller machine
    golang use net lib to visit player HTTP API
    golang use rpc lib to deploy sdk to SDK machines
    golang use net lib to visit player result

### Player machine
    Ranorex to control player and collect result
    golang use revel to provide HTTP API
    
### SDK machine
    Large Hard Disk for long time running logs
    Only run one SDK each machine
    SDK root path is /root/sdk
    
    
## Deployment
### Windows Flash Player Machine
    IP address: 192.168.2.7
    User and Password: xuguoqinag/123456
    Reboot windows via mstsc: open cmd; shutdown -r    
    Installation: Ranorex, IE, Windows flash debugger player
    Installation: golang 1.8.3
    C:\cybertron\Controller>bin\revel.exe run playServer
    http://localhost:9000/public/player/live_test.swf

### Linux LivePush Machine
    IP address: 192.168.3.174
    User and Password: root/root
    Push dir: /push/live-push-server
    Start push: ./start.sh


### Linux SDK Machine
    IP address: 192.168.100.201-205
    User and Password: root/root
    SDK dir: /test/sdk


## Run Test
### Linux Controller Machine
    IP address: 192.168.100.200
    User and Password: root/root
    . replace sdk: update sdk at /root/sdk (with log)
    . cd /root/Controller/src
    . start sdk: go run main.go -start_sdk
    . stop sdk:  go run main.go -stop_sdk
    . start play: go run main.go -start_play
    . stop Ranorex: go run main.go -stop_play
    . check result: go run status.go

