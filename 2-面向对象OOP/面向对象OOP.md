# C# 面向对象（OOP）

> 配套位置：`2-面向对象OOP`
> 前置：`1-语法基础`
> 目标：会用类与对象建模，掌握封装、继承、多态，能区分抽象类与接口，写出可维护的代码。
> 约定：示例均为可运行的顶层语句代码（.NET 6+）。若写成 `.cs` 文件，可放在 `Program.cs` 里直接运行；类一般定义在文件末尾。

---

## 目录

1. [类与对象](#1-类与对象)
2. [字段与属性](#2-字段与属性)
3. [访问修饰符](#3-访问修饰符)
4. [构造函数与析构函数](#4-构造函数与析构函数)
5. [静态成员与 static 类](#5-静态成员与-static-类)
6. [const vs readonly](#6-const-vs-readonly)
7. [封装](#7-封装)
8. [继承](#8-继承)
9. [多态](#9-多态)
10. [虚方法 virtual / override / new](#10-虚方法-virtual--override--new)
11. [base 与 this](#11-base-与-this)
12. [抽象类 vs 接口](#12-抽象类-vs-接口)
13. [嵌套类、partial、分部方法](#13-嵌套类partial分部方法)
14. [对象初始化器与索引器](#14-对象初始化器与索引器)
15. [综合练习](#15-综合练习)

---

## 1. 类与对象

**类**是模板（设计图），**对象**是按模板造出来的实例。

```csharp
// 定义类
public class Person
{
    public string Name = "未知";
    public int Age;

    public void SayHello()
    {
        Console.WriteLine($"你好，我是 {Name}，今年 {Age} 岁");
    }
}

// 创建对象（new）
Person p1 = new Person();
p1.Name = "小明";
p1.Age = 18;
p1.SayHello();      // 你好，我是 小明，今年 18 岁

Person p2 = new Person();
p2.SayHello();      // 你好，我是 未知，今年 0 岁
```

> 关键：`new` 分配对象，变量 `p1` 存的是对象的**引用**（见语法基础第 10 节）。

---

## 2. 字段与属性

- **字段**：类里直接声明的变量，通常私有。
- **属性**：对外暴露的“访问器”，本质是 `get/set` 方法。

```csharp
public class Student
{
    private int _age;                 // 字段（私有）

    // 完整属性：可在 get/set 里加逻辑
    public int Age
    {
        get { return _age; }
        set
        {
            if (value < 0) throw new ArgumentException("年龄不能为负");
            _age = value;
        }
    }

    // 自动属性：编译器自动生成隐藏字段（最常用）
    public string Name { get; set; } = "无名";

    // 只读属性（只能在构造函数里赋值）
    public string Id { get; }

    // init：只能在对象初始化时赋值，之后不可改
    public string Grade { get; init; } = "一年级";

    public Student(string id) => Id = id;
}

var s = new Student("S001") { Name = "小红", Age = 12 };
Console.WriteLine($"{s.Id} {s.Name} {s.Age} {s.Grade}");
// s.Age = 13;   // 可以（有 set）
// s.Grade = "二年级"; // 编译错误（init 只能初始化时赋值）
```

### 属性写法速查

| 写法 | 含义 |
|---|---|
| `public int Age { get; set; }` | 可读可写自动属性 |
| `public int Age { get; }` | 只读（构造函数赋值） |
| `public int Age { get; private set; }` | 外部只读，类内部可写 |
| `public int Age { get; init; }` | 仅对象初始化时可赋值 |
| `public int Age => _age;` | 表达式体只读属性 |
| `public int Age { get; set; } = 18;` | 带默认值 |

---

## 3. 访问修饰符

| 修饰符 | 说明 |
|---|---|
| `public` | 任何地方都能访问 |
| `private` | 只有本类能访问（默认） |
| `protected` | 本类及**子类**能访问 |
| `internal` | 同一程序集（项目）内可访问 |
| `protected internal` | protected 或 internal 满足其一 |
| `private protected` | 同一程序集内且是子类 |

```csharp
public class Account
{
    private decimal _balance;          // 只有本类可用
    protected string Owner;            // 子类可用
    internal string Branch = "总行";   // 同项目可用
    public decimal Balance => _balance;

    public void Deposit(decimal amount) => _balance += amount;
}
```

> 原则：**能用 private 就不要 public**，减少对外暴露。

---

## 4. 构造函数与析构函数

**构造函数**：创建对象时自动调用，用于初始化。**析构函数**：对象被回收前调用（很少用，除非持有非托管资源）。

```csharp
public class Order
{
    public string Id { get; }
    public DateTime Created { get; }

    // 无参构造
    public Order()
    {
        Id = Guid.NewGuid().ToString("N")[..8];
        Created = DateTime.Now;
    }

    // 带参构造
    public Order(string id)
    {
        Id = id;
        Created = DateTime.Now;
    }

    // 构造函数链：先调用无参构造，再执行自己
    public Order(string id, bool fromRepo) : this(id)
    {
        Console.WriteLine("来自数据库");
    }

    // 静态构造函数：只执行一次，第一次用到该类时
    static Order()
    {
        Console.WriteLine("Order 类被首次使用");
    }

    // 析构函数（了解即可）
    ~Order() { /* 释放非托管资源 */ }
}

var o = new Order("A100");
Console.WriteLine($"{o.Id} {o.Created:HH:mm:ss}");
```

> 推荐用 `IDisposable`（第 5 章异常处理）管理资源，而不是析构函数。

---

## 5. 静态成员与 static 类

静态成员属于**类本身**，不属于某个对象，通过 `类名.成员` 访问。

```csharp
public class MathUtil
{
    public static double Pi = 3.14159;      // 静态字段

    public static double CircleArea(double r) => Pi * r * r;   // 静态方法

    public static int Count { get; private set; }   // 静态属性

    static MathUtil() => Console.WriteLine("静态构造执行");
}

Console.WriteLine(MathUtil.CircleArea(2));   // 12.56636

// 静态类：只能有静态成员，不能实例化 new
public static class Constants
{
    public const string AppName = "学习系统";
}

Console.WriteLine(Constants.AppName);
```

**何时用 static**：工具方法、全局常量、计数器、单例。滥用会让代码难测试。

---

## 6. const vs readonly

| 对比 | `const` | `readonly` |
|---|---|---|
| 赋值时机 | 编译期确定 | 运行期（声明处或构造函数） |
| 能否改 | 永远不能 | 构造函数里可赋值一次 |
| 修饰对象 | 只能基本类型、string | 任意类型 |
| 默认是 | 隐式 static | 实例或 static |

```csharp
public class Config
{
    public const double TaxRate = 0.13;      // 编译期常量，隐式 static
    public readonly DateTime StartTime;      // 运行期确定

    public Config()
    {
        StartTime = DateTime.Now;            // 只能在构造函数里赋值
    }
}

var c = new Config();
Console.WriteLine($"{Config.TaxRate} {c.StartTime:HH:mm:ss}");
```

> 口诀：**值永远不变用 const，运行时才确定用 readonly。**

---

## 7. 封装

把数据藏起来，只通过公开方法访问，防止外部乱改。

```csharp
public class BankAccount
{
    private decimal _balance;                 // 私有字段

    public decimal Balance => _balance;       // 只读属性对外暴露

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("存款必须为正");
        _balance += amount;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= 0 || amount > _balance) return false;
        _balance -= amount;
        return true;
    }
}

var acc = new BankAccount();
acc.Deposit(100);
Console.WriteLine(acc.Withdraw(30));   // True
Console.WriteLine(acc.Withdraw(1000)); // False
Console.WriteLine(acc.Balance);        // 70
// acc._balance = 99999;   // 编译错误：外部无法直接改
```

> 封装的价值：**数据只能按你定的规则变化**，外部无法绕过校验。

---

## 8. 继承

子类用 `:` 继承父类，获得父类的成员，并可扩展。

```csharp
public class Animal
{
    public string Name { get; set; } = "";
    public void Eat() => Console.WriteLine($"{Name} 在吃东西");
}

public class Dog : Animal          // Dog 继承 Animal
{
    public void Bark() => Console.WriteLine($"{Name} 汪汪叫");
}

var d = new Dog { Name = "旺财" };
d.Eat();    // 旺财 在吃东西（继承来的）
d.Bark();   // 旺财 汪汪叫
```

- C# 是**单继承**：一个类只能有一个直接父类。
- 所有类最终都继承自 `object`。
- 父类应尽量设计得简洁；深层继承链难维护，偏爱组合。

---

## 9. 多态

同一个方法调用，**不同对象有不同表现**。是 OOP 最核心的价值。

```csharp
public abstract class Shape
{
    public abstract double Area();      // 抽象方法，子类必须实现
    public void Describe() =>
        Console.WriteLine($"{GetType().Name} 面积 = {Area():F2}");
}

public class Circle : Shape
{
    public double R { get; set; }
    public override double Area() => Math.PI * R * R;
}

public class Rectangle : Shape
{
    public double W { get; set; }
    public double H { get; set; }
    public override double Area() => W * H;
}

// 关键：用父类型保存子类对象，运行时调用各自实现
Shape[] shapes =
{
    new Circle    { R = 2 },
    new Rectangle { W = 3, H = 4 }
};

foreach (var shape in shapes)
    shape.Describe();
// Circle 面积 = 12.57
// Rectangle 面积 = 12.00
```

> 多态让代码**面向父类编程**，将来新增形状不用改 `foreach`。

---

## 10. 虚方法 virtual / override / new

| 关键字 | 作用 |
|---|---|
| `virtual` | 父类中“允许被重写”的方法 |
| `override` | 子类重写父类的虚方法（多态） |
| `new` | 子类**隐藏**父类同名方法（不推荐，易混淆） |
| `sealed override` | 重写后禁止再被继承者重写 |

```csharp
public class Base
{
    public virtual void Show() => Console.WriteLine("Base.Show");
    public void Hide() => Console.WriteLine("Base.Hide");
}

public class Derived : Base
{
    public override void Show() => Console.WriteLine("Derived.Show");  // 重写
    public new void Hide() => Console.WriteLine("Derived.Hide");        // 隐藏
}

Base b = new Derived();
b.Show();   // Derived.Show  ← 多态生效
b.Hide();   // Base.Hide     ← 隐藏不参与多态，看变量的编译期类型

Derived d = new Derived();
d.Hide();   // Derived.Hide
```

> 记住：**override 看对象实际类型，new 看变量声明类型。** 日常优先用 override，少用 new。

---

## 11. base 与 this

```csharp
public class Employee
{
    public string Name { get; }
    public Employee(string name) => Name = name;
    public virtual void Work() => Console.WriteLine($"{Name} 在工作");
}

public class Manager : Employee
{
    public string Dept { get; }

    // base(...)：调用父类构造函数
    public Manager(string name, string dept) : base(name)
    {
        Dept = dept;
    }

    public override void Work()
    {
        base.Work();     // base. 调用父类实现
        Console.WriteLine($"作为 {Dept} 经理在管理");
    }
}

new Manager("老王", "研发").Work();
// 老王 在工作
// 作为 研发 经理在管理
```

- `this`：指当前对象，也用于构造函数链 `: this(...)`。
- `base`：指父类，访问父类成员或构造函数。

---

## 12. 抽象类 vs 接口

两者都用于“约定能力”，但用途不同。

| 对比 | 抽象类 abstract class | 接口 interface |
|---|---|---|
| 能否实例化 | 否 | 否 |
| 方法实现 | 可以有（含抽象方法） | 传统上只有声明；C# 8+ 可有默认实现 |
| 字段/构造函数 | 可以有 | 不能有实例字段、构造函数 |
| 继承数量 | 只能继承 1 个 | 可实现多个 |
| 关系语义 | “是一个” (is-a) | “能做/具备” (can-do) |
| 适用 | 有共同代码和状态的基类 | 定义跨类别的能力契约 |

```csharp
// 抽象类：共享代码 + 强约束
public abstract class Vehicle
{
    public int Speed { get; protected set; }
    public abstract void Move();
    public void Stop() => Console.WriteLine("停车");  // 公共实现
}

// 接口：定义能力
public interface IChargeable
{
    void Charge();
}

public interface IRepairable
{
    void Repair();
}

public class ElectricCar : Vehicle, IChargeable, IRepairable
{
    public override void Move() => Console.WriteLine("电动车行驶");
    public void Charge() => Console.WriteLine("充电中");
    public void Repair() => Console.WriteLine("维修中");
}

var car = new ElectricCar();
car.Move();
car.Charge();
car.Stop();
```

> 选择：**需要共享实现/状态 → 抽象类；只需要约定能力、要多个 → 接口。**
> 常见做法：接口为主，抽象类为辅。

---

## 13. 嵌套类、partial、分部方法

```csharp
// 嵌套类：类里定义类，适合只服务外层的辅助类型
public class Outer
{
    public class Inner
    {
        public void Hi() => Console.WriteLine("我是内部类");
    }
}
new Outer.Inner().Hi();

// partial：把一个类拆到多个文件/位置（分部类）
public partial class Customer
{
    public string Name { get; set; } = "";
}
public partial class Customer
{
    public void Print() => Console.WriteLine(Name);
}
new Customer { Name = "张三" }.Print();   // 张三
```

> `partial class` 常用于把「自动生成的代码」和「手写代码」分开。
> `partial method`（分部方法）用于拆分方法的声明与实现，理解即可。

---

## 14. 对象初始化器与索引器

### 对象初始化器

```csharp
public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

var p = new Point { X = 3, Y = 4 };    // 不用写构造函数即可赋值
Console.WriteLine($"{p.X},{p.Y}");     // 3,4
```

### 索引器（用 `[]` 访问对象）

```csharp
public class Scores
{
    private readonly int[] _data = new int[5];

    public int this[int i]
    {
        get => _data[i];
        set => _data[i] = value;
    }
}

var sc = new Scores();
sc[0] = 90;
sc[1] = 85;
Console.WriteLine($"{sc[0]}, {sc[1]}");   // 90, 85
```

---

## 15. 综合练习

### 练习 1：图形面积体系
定义抽象类 `Shape`（含 `Area()`），派生 `Circle`、`Rectangle`、`Triangle`，用多态数组遍历输出面积。

### 练习 2：银行账户
实现 `BankAccount`（私有余额、存款、取款、余额只读属性），禁止非法操作。

### 练习 3：员工与经理
`Employee` 与 `Manager` 继承，`Manager` 用 `base` 扩展 `Work()`；再加接口 `IManage`。

### 练习 4：只读与 init
设计 `Order` 类，`Id` 只读、`Status` 用 `init`、`Amount` 用带校验的属性。

### 练习 5：静态工具类
写 `StringHelper` 静态类，提供 `IsPalindrome`、`Reverse`、`CountWords`。

### 练习 6：接口多态
定义 `IPayable` 接口（`Pay()`），实现 `WechatPay`、`AlipayPay`、`CardPay`，用 `List<IPayable>` 遍历支付。

### 练习 7：索引器
实现 `Matrix` 类，用 `matrix[row, col]` 访问二维数据。

---

## 自测清单

- [ ] 我能区分类和对象，会 `new` 出实例
- [ ] 我会写自动属性、只读属性、`init` 属性
- [ ] 我能说出各访问修饰符的范围
- [ ] 我会写构造函数、构造函数链、静态构造函数
- [ ] 我理解 static 成员的用途与隐患
- [ ] 我能区分 `const` 和 `readonly`
- [ ] 我会用私有字段 + 属性实现封装与校验
- [ ] 我会用继承复用父类成员
- [ ] 我能解释多态，并写出 `virtual/override` 示例
- [ ] 我能说出 `override` 与 `new` 的区别
- [ ] 我会用 `base` 调用父类
- [ ] 我能判断什么时候用抽象类、什么时候用接口
- [ ] 我会写分部类、对象初始化器和索引器
