# AndroidApkClearLog
A C# project that remove android source code Log.

这个工程主要用来删除android 源代码里的Log代码，如Log.d("tag", "debug info");

命令行运行:

rmcode.exe srcfolder keyword outfolder

暂时不支持绝对路径.

rmcode.exe与srcfolder需要在同一目录.

生成的结果会以同样的目录层叠形式存放在outfolder下面.

keyword是要删除的log函数前面的关键字,例如删除:

Log.d("tag", "debug info");

rmcode.exe src Log.d outfolder
