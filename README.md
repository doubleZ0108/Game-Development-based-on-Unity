# Unity知识点🧀️

[toc]

------

## Basic Structure of a Unity Game

- `GameObjects` server as 'containers'
  - holding  `Components`
- `Component`: the basic unit(structure) of game logic

<img src="README.assets/architecture.png" alt="architecture" width="40%;" />

<br />

## GameObject

### Hierarchy - Parenting

- Parent will affect the coordinate calculating of child

- child的变换都是相对于parent的

  > 例如partent改变了scale属性，则child的坐标系就不再是标准笛卡尔坐标系，各坐标轴的单位长度将不一致

<br />

## Game

- 在调试游戏的时候要设置游戏的比例/屏幕的尺寸



<br />

## Inspector

- 可以将该窗口切换为Debug模式，可以看到更多后台实际的变量情况
- 通常Normal模式下的参数都是为了直观的看到影响



<br />

## Coordinate System

⚠️以下调整都需要在`Pivot`模式下进行<img src="README.assets/image-20200316151329284.png" alt="image-20200316151329284" width="20%;" />

### Tricks Scaling

<img src="README.assets/image-20200316144953074.png" alt="image-20200316144953074" width="30%;" />

1. 正常建完模型由于child的坐标系是基于parent的，而parent改变了scale，导致child坐标系不是笛卡尔坐标系，所以旋转起来每个轴动的尺寸不一样(local coordinate system become non-Cartesian, axis are different in unit length)

  > **【解决方法】**添加 Adaptor 进行Normalize
  >
  > 1. 初始object tree
  >
  >    <img src="README.assets/image-20200316145921162.png" alt="image-20200316145921162" width="30%;" />
  >
  > 2. 把Turret从树中拉出来，创建一个新的TurretAdaptor，将其`Position`全设置为0  => 将Turret放到TurretAdaptor中 => 再将TurretAdaptor放到Torso中
  >
  >    <img src="README.assets/image-20200316145853874.png" alt="image-20200316145853874" width="20%;" /><img src="README.assets/image-20200316145834514.png" alt="image-20200316145834514" width="50%;" />
  >
  > 3. Turret可能会fly到一个随机位置，把它重新拖回来就好了
  >
  > 
  >
  > **【原理】**
  >
  > Torse中`Scale`设置的是(6.79 1.25 6.00)
  >
  > 添加的Adaptor中`Scale设置的是`(0.14 0.79 0.16)
  >
  > 点乘结果刚好为(1.0 1.0 1.0)，将parent的变换抵消掉，重新回到笛卡尔坐标系

<img src="README.assets/image-20200316150447023.png" alt="image-20200316150447023" width="25%;" />

2. 炮台的旋转中心不是我们期待的位置

  > **【解决方法】**再使用一个Adaptor将这个child依照的坐标系中心点移动一个位置
  >
  > 1. 同样将Gun拉出来
  > 2. 创建一个GunHinge(铰链)，将`Position`设置为(0,0,0)
  > 3. 再将Gun放进去，再将这部分与整体合并
  >
  > **旋转的时候旋转Hinge**



<br />

## Scripts

- `MonoBehaviour` is Custom Components in script-level

- 声明的public变量直接在Inspector中拖动赋值

- 获取场景中的对象可以用`GameObject`也可以用`Transform`，这二者功能上很像

  - 比如要设置对象的位置`gameObject.transform.position` 或 `transform.position`

- **监听**

  - **键盘**: 注意else的使用，默认的时候应该允许用户同时按两个按键

    ```c#
    /* 按下时一直触发 （按帧执行）*/
    if (Input.GetKey(KeyCode.W))
    {
      this.transform.Translate(Time.deltaTime * 1.0f, 0.0f, 0.0f);
    }
    
    if (Input.GetKey(KeyCode.S))
    {
      this.transform.Translate(Time.deltaTime * -1.0f, 0.0f, 0.0f);
    }
    ```

- **Transform**

  - **平移**

    ```c#
    this.transform.Translate(Time.deltaTime * 1.0f, 0.0f, 0.0f, Space.Self);
    this.transform.localPosition += transform.forward * 1.0f * Time.deltaTime;	//z轴方向为forward
  ```
  
- **旋转**
  
    ```c#
    transform.Rotate(0.0f, Time.deltaTime * -20.0f, 0.0f, Space.Self);
  ```
  
    - **获取当前object的旋转信息**：`transform.eulerAngles`  `transform.localEulerAngles` （直接获取rotation得到的是四元组）
    
      >  ⚠️欧拉角的范围是[0, 360]，使用的时候要进行相应的转换
      >
      > 所以如果`if(gunHinge.eulerAngles.z > -4.0f)`永远为真
    
  - **放缩**

