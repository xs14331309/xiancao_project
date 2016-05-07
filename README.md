###现操期中项目

+ 用github进行版本控制和团队开发:
  * 使用方法：
     1. 安装git
     2. 在github中搜索 “xiancao_project" 找到我们的project，然后fork到自己的本地仓库
     3. 到自己的本地仓库中复制仓库地址
     4. 打开vs，在团队资源管理器中克隆你fork了的仓库 [tips](https://msdn.microsoft.com/zh-cn/library/hh850445.aspx#remote_3rd_party_connect_clone)
     5. 平时的提交操作和git差不多， “更改” 中提交，然后在 “同步” 中可以推送。 
     [终极tips](https://msdn.microsoft.com/en-us/library/vs/alm/code/git/get-started)
     

+ todo：
  1. 注册 登录页面
  2. 发布任务页面
  3. 任务详情页面 （后期考虑加入评论）
  4. 主页面待完善
  5. 页面之间的跳转：
    - LoginPage 登录到 MainPage
    - LoginPage 注册到 SignUpPage
    - SignUpPage 跳回 LoginPage
    - MainPage页面跳转到 AddTaskPage
    - MainPage页面跳转到 TaskListPage
    - TaskListPage跳转到TaskContentPage
