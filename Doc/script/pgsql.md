
# postgresql 常用命令

## 备份

```bin
pg_dump -h localhost -U postgres imt_luthai > imt_luthai_20211129.sql
```
-U：指定用户名
-W：指定密码
-F ：指定备份格式，默认备份文件为sql格式

##还原
psql -h 192.168.0.103 -U postgres -d linuxe -f linxue.sql
