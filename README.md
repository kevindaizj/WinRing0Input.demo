<h1 align="center">WinRing0驱动级输入</h1>

<div align="center">
  针对密码控件的模拟输入demo
</div>

<br/>

### 配置
1. Properties => Build => disable "prefer 32-bits" (use x64 dll)
2. Properties => Build Events => Post-build => 编译成功则复制Dll到bin目录。
3. 使用**管理员权限**运行
4. 输入法调整为美式英文键盘（待改进？）


<br/>


### 参考（抄袭）
1. [C# 模拟键盘输入](https://www.jianshu.com/p/a0c88a765bfd)
2. [C# 模拟键盘输入源码参考（Github）](https://github.com/Zzz2333/TestKeyboard)
3. [ascii转为VirtualKey（Stackoverflow）](https://stackoverflow.com/questions/2934557/convert-character-to-the-corresponding-virtual-key-code)

