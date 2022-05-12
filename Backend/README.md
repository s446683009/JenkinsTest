
# 后端代码
1.Identity 构建

切换到根目录
```docker

docker image build -f  './src/Services/Identity/Identity.Api/Dockerfile' -t identity .
 docker run -d -t --rm -p 83:80 --name scr_identity identity 

```
