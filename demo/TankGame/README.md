# Tank Game

* [Model](#model)
   * [Coordinate System Scaling](#coordinate-system-scaling)
* [Tank-Style Maneuver Logic](#tank-style-maneuver-logic)
   * [1. Add public fields and Assign objects](#1-add-public-fields-and-assign-objects)
   * [2. Movement](#2-movement)
   * [3. Turret Behavior](#3-turret-behavior)
   * [4. Shooting Logic](#4-shooting-logic)
* [Refactor Controlling Logic](#refactor-controlling-logic)
   * [Tank](#tank)
   * [TankPlayerInputHandler](#tankplayerinputhandler)
   * [Register](#register)

------

<img src="https://upload-images.jianshu.io/upload_images/12014150-3412a91cc3fe1554.gif?imageMogr2/auto-orient/strip" width="60%" />

<br />

## Model

<img src="README.assets/image-20200316201219577.png" alt="image-20200316201219577" width="60%;" />

### Coordinate System Scaling

⚠️以下调整都需要在`Pivot`模式下进行<img src="../../README.assets/image-20200316151329284.png" alt="image-20200316151329284" width="20%;" />

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

   <br />

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

## Tank-Style Maneuver Logic

### 1. Add public fields and Assign objects

<img src="README.assets/image-20200316202137434.png" alt="image-20200316202137434" width="50%;" />

### 2. Movement

- 键盘监听

- 移动 后退

- 移动

  ```c#
  this.transform.Translate(Time.deltaTime * 1.0f, 0.0f, 0.0f, Space.Self);
  ```

### 3. Turret Behavior

- 原地旋转 (该脚本绑在Tank身上，所以全体都会跟着移动)

- 炮塔旋转

- 炮台俯角仰角

  > ⚠️欧拉角的范围是[0, 360]，使用的时候要进行相应的转换
  >
  > 所以如果`if(gunHinge.eulerAngles.z > -4.0f)`永远为真

### 4. Shooting Logic

- 设置炮弹的Rigidbody属性

```c#
Rigidbody freshMuzzle = Instantiate<Rigidbody>(muzzlePrefab);   //复制一个prefab炮弹
freshMuzzle.transform.position = muzzle.position;               //设置位置为标定的炮口位置
freshMuzzle.velocity = muzzle.forward * 10.0f;                  //设置发射初速度
```

<br />

------

## Refactor Controlling Logic

- Tank定义具体游戏逻辑而忽略Input，只在内部对Handler进行注册
- InputHandler处理Input

### Tank

- **具体游戏逻辑**

  - `Move()`
  - `Rotate()`
  - `RotateTurret()`
  - `RotateGun()`
  - `Fire()`

- **注册InputHandler**

  ```c#
  void Start()
  {
    TankPlayerInputHandler handler = TankPlayerInputHandler.Instance();
    handler.Axis1VerticalInputEvent += Move;
  	//...
    handler.FireInputEvent += Fire;
  }
  ```

### TankPlayerInputHandler

- global accessible controller

- **Singleton**: make a class still non-static, but accessible every where in script level

  ```c#
  /* Singleton */
  private static TankPlayerInputHandler _sInstance = null;
  
  public static TankPlayerInputHandler Instance()
  {
    _sInstance = FindObjectOfType<TankPlayerInputHandler>();        //保证整个场景只有一个
    if(_sInstance == null)
    {
      GameObject newObj = new GameObject(name: "TankPlayerInputHandler");
      _sInstance = newObj.AddComponent<TankPlayerInputHandler>();
    }
    return _sInstance;
  }
  ```

- **add Events**

  ```c#
  public event Action<float> Axis1HorizontalInputEvent;
  //...
  public event Action FireInputEvent;
  ```

- **处理Input Event**

  ```c#
  void Update()
  {
    /* 前进 */
    if (Input.GetKey(KeyCode.W))
    {
      if(Axis1VerticalInputEvent != null)
      {
        Axis1VerticalInputEvent(1.0f);
      }
    }
    //...
    //...
  }
  ```

  

### Register

if there is 2 tanks in the scene, which to control?

点击选中一辆坦克，可以控制它，再次点击取消选择；可以同时选择多个，互不影响

- 不再在`Start()`中直接注册Action

- Event System -> IPointerClickHandler

- **TankPlayerInputHandler**

  ```c#
  public event Action<bool> SetControlActiveEvent;
  
  public void TakeTankControl(Tank tank)
  {
    if (SetControlActiveEvent != null)
    {
      SetControlActiveEvent(false);
    }
    tank.SetControlActive(true);
  }
  ```

- **Tank**

  ```c#
  private bool isControlActive;
  
  public void SetControlActive(bool isActive)
  {
    TankPlayerInputHandler handler = TankPlayerInputHandler.Instance();
  
    if (isActive != isControlActive)    //第一次点击 => 注册
    {
      isControlActive = isActive;
      handler.Axis1VerticalInputEvent += Move;
      //...
    }
    else
    {
      isControlActive = !isActive;    //再次点击 => 移除注册信息
      handler.Axis1VerticalInputEvent -= Move;
      //...
    }
  }
  
  public void OnPointerClick(PointerEventData eventData)
  {
    TankPlayerInputHandler.Instance().TakeTankControl(this);
  }
  ```

  

