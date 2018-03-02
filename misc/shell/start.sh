ps aux|grep livepush | grep -v grep|awk  '{print $2}'|xargs kill -9
(./livepush > push.log 2>&1 &)
