1. git 指定目录
git init
git remote add -f  origin https://github.com/Yourens/decaf_PA2_2018.git
git config core.sparseCheckout true
echo 'TestCases' >> .git/info/sparse-checkout  
git pull origin master
