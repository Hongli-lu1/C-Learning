# C# 学习清单（可勾选进度）

> 用法：把 `[ ]` 改成 `[x]` 表示已掌握。按顺序学，遇到卡住先跳过后回补。

---

## 0. 环境与工具
- [ ] 安装 .NET SDK（LTS）
- [ ] 安装 Visual Studio 2022 或 VS Code + C# Dev Kit
- [ ] 会用 `dotnet new / build / run / publish`
- [ ] 会建 Console / Class Library / WinForms / WPF 项目
- [ ] 理解 .sln / .csproj 结构
- [ ] 会调试：断点、条件断点、监视、调用堆栈、即时窗口
- [ ] 安装 Git 并会提交

## 1. 语法基础
- [ ] 变量、常量、var、基本类型、字面量
- [ ] 运算符（算术/比较/逻辑/位/三元）
- [ ] 流程控制（if/switch/for/while/foreach/break/continue）
- [ ] 字符串（$插值、@、StringBuilder、常用方法）
- [ ] 数组（一维/多维/交错）
- [ ] 枚举 enum、结构体 struct
- [ ] 方法（重载、默认参数、params、ref/out/in）
- [ ] 类型转换（隐式/显式/Convert/Parse/TryParse）
- [ ] 值类型 vs 引用类型
- [ ] 命名规范与代码风格

## 2. 面向对象（OOP）
- [ ] 类、对象、字段、属性（get/set、自动属性、init）
- [ ] 访问修饰符（public/private/protected/internal）
- [ ] 构造/析构、静态成员、static 类、const vs readonly
- [ ] 封装、继承、多态
- [ ] 抽象类 vs 接口
- [ ] virtual / override / new / base / this
- [ ] 嵌套类、partial、分部方法
- [ ] 对象初始化器、索引器

## 3. 泛型、集合、LINQ
- [ ] 泛型类/方法/约束、协变逆变
- [ ] List / Dictionary / HashSet / Queue / Stack / LinkedList
- [ ] IEnumerable / ICollection / IList / IDictionary
- [ ] LINQ：Where/Select/OrderBy/GroupBy/Join/Any/First/Single/Sum/Aggregate
- [ ] 延迟执行、yield return
- [ ] 迭代器、IComparable / IComparer

## 4. 委托、事件、Lambda
- [ ] delegate、Action、Func、Predicate
- [ ] 多播委托、匿名方法、Lambda
- [ ] event 事件、发布订阅模式
- [ ] 闭包与变量捕获

## 5. 异常处理与调试
- [ ] try/catch/finally、throw、when 过滤器
- [ ] 内置异常、自定义异常
- [ ] using 与 IDisposable
- [ ] 断言、日志、防御式编程
- [ ] 异常 vs 返回值的选择

## 6. 字符串、文件与序列化
- [ ] File / Stream / FileStream / StreamReader / StreamWriter
- [ ] Directory / Path
- [ ] JSON（System.Text.Json）、XML、二进制
- [ ] 配置文件读写

## 7. 多线程与异步 ★重点
- [ ] Thread、线程池、前台/后台线程
- [ ] Task、Task<T>、async/await、ConfigureAwait
- [ ] Parallel、PLINQ
- [ ] lock、Monitor、Mutex、Semaphore、Interlocked
- [ ] ConcurrentDictionary、BlockingCollection
- [ ] CancellationToken 取消
- [ ] 死锁、竞态、线程安全概念

## 8. 内存与性能
- [ ] 栈 vs 堆、GC 原理、代龄
- [ ] IDisposable / 终结器
- [ ] 装箱拆箱开销
- [ ] Span<T> / Memory<T> / stackalloc / unsafe
- [ ] 性能分析（Stopwatch / BenchmarkDotNet / VS 诊断）

## 9. 反射、特性与元编程
- [ ] Attribute 定义与使用
- [ ] 反射 Type / Assembly / 动态创建对象
- [ ] dynamic（了解）、表达式树（了解）
- [ ] 依赖注入（Microsoft.Extensions.DependencyInjection）

## 10. 现代 C# 特性
- [ ] record / record struct
- [ ] 模式匹配（is / switch 各模式）
- [ ] 可空引用类型 nullable
- [ ] 顶层语句、required、init
- [ ] 局部函数、using 声明、目标类型 new()
- [ ] 集合表达式、原始字符串

## 11. 数据库与网络
- [ ] ADO.NET、参数化 SQL
- [ ] SQLite
- [ ] Entity Framework Core
- [ ] HttpClient
- [ ] TCP / UDP Socket
- [ ] 异步 IO

## 12. 桌面 UI
- [ ] WinForms 控件 / 事件 / 布局 / 对话框
- [ ] WPF：XAML / 数据绑定 / 依赖属性 / 命令 / 样式 / 模板
- [ ] MVVM
- [ ] MVC / MVP / MVVM 对比

## 13. 工程化与设计模式
- [ ] Git：分支 / 合并 / 冲突 / .gitignore
- [ ] 设计模式：单例 / 工厂 / 策略 / 观察者 / 状态 / 适配器 / DI
- [ ] 分层架构 UI / 业务 / 数据
- [ ] 日志 NLog / Serilog
- [ ] 单元测试 xUnit / NUnit / Mock
- [ ] 打包发布（dotnet publish / 单文件）

## 14. 综合练手项目
- [ ] 控制台计算器 / 猜数字
- [ ] 通讯录（JSON 持久化 + 增删改查）
- [ ] 记账本（SQLite + EF Core）
- [ ] 多线程文件搜索 / 下载器
- [ ] 待办事项（WinForms/WPF + MVVM）
- [ ] HTTP 客户端 / 天气查询
- [ ] 聊天室（TCP Socket 多客户端）
- [ ] 小游戏（贪吃蛇 / 俄罗斯方块）
