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

- **Rect Transform**：represent a 2D area instead of a single point

  - dynamic UI layout
  - **Pivot**: 旋转轴；其他object开起来的位置
  - **Anchor**：control how each edge of the area 



<br/>

------

## RectTransform & Layout Control Components

- Horizontal / Vertical / Grid Layout Group
- LayoutElement: 控制layout中的子物体

<br/>

------

## UI Widgets

### Text

### Image/RawImage

### Mask

### Button/Toggle

### Scroll View

### Slider

### Input Field

### Dropdown



<br/>

------

## RaycastTarget, Interactable & CanvasGroup



<br/>

------

## Sort Order



<br/>

------

## Event System