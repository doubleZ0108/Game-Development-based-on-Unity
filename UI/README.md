# UI

[toc]

------

## Classification

- **Legacy GUI (IMGUI)**
  - still uses this syste
    - custom editor window
    - custom inspector UI
- **NGUI**
  - 3rd Party UI Library 
- **UGUI**

<br/>

------

## Canvas

- container of all UGUI widgets

- **Render Mode**

  - **screen space overlay**: 默认会覆盖住屏幕(永远在最上面)
  - **screen space camera**: 
    - 需要在场景中布置camera
    - 可以产生景深效果
  - **world space**: 3D面片放在空间场景里（完全自由）

  <img src="README.assets/image-20200325140857897.png" alt="image-20200325140857897" width="50%;" />

- **Sort Order**: 哪个canvas先画，哪个后画

- **Canvas Scaler**：自适应多分辨率

- **Graphic Raycaster**：用户的点击射线第一个碰到哪个组件

<br/>

------

## RectTransform & Layout Control Components

- **Rect Transform**：represent a 2D area instead of a single point
  - dynamic UI layout
  - **Pivot**: 旋转轴；其他object开起来的位置
  - **Anchor**：control how each edge of the area 
- **Horizontal** / **Vertical** / **Grid Layout Group**
- **LayoutElement**: 控制layout中的子物体

<br/>

------

## UI Widgets

### Text

- work as a <u>label</u>, user cannot edit the text directly in game
- richtext: bold, italic, ...

### Image/RawImage

- **RenderTexture**: 将当前camera的图像存下来，作为RawImage的输入（画中画）
- RawImage is not interactable, but can add a transparent Text/Image over it to receive user input
- **Sprite**: text和image的中间层
- **九宫格**

### Mask

- masking display area of child widgets(mask的子物体受mask影响)

  > 周围有滚动条，只显示一部分

### Button/Toggle

- **Button**：combine of button and image
  - OnClickHandler callbacks不建议，最好在代码中动态的注册
  - some built-in effect setting for interaction
- **Toggle**
  - OnValueChangeHandler
- **ToggleGroup**
  - 这个object的子物体如果有Toggle，一律被认为是Radio

### Scroll View

- consits of a ScrollRect and two Scrollbar components

  <img src="README.assets/image-20200325152757691.png" alt="image-20200325152757691" width="50%;" />

  - add sub-items under `Content` GameObject

### Slider

- provide a result between Min&Max Value

### Input Field

- not supporting rich text
- support limiting content type

### Dropdown

- templage is referenced to clone multiple sub-items for each option in the dropdown list

<br/>

------

## RaycastTarget, Interactable & CanvasGroup



<br/>

------

## Sort Order



<br/>

------

## Event System