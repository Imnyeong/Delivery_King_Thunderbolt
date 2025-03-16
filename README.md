### DELIVERY KING THUNDERBOLT
![icon](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white) ![icon](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white) ![icon](https://img.shields.io/badge/Firebase-F29D0C?style=for-the-badge&logo=firebase&logoColor=white) ![icon](https://img.shields.io/badge/Google_Play-414141?style=for-the-badge&logo=google-play&logoColor=white) ![icon](https://img.shields.io/badge/App_Store-0D96F6?style=for-the-badge&logo=app-store&logoColor=white)

## 개요 📝
가속도 센서를 활용하여 단말기의 기울기로 조작하는 2D 모바일 게임

## Tech Stack ✏️
- Unity
- C#
- Firebase
- Visual Studio
- Sourcetree

## 기술 🔎
- 가속도 센서로 기울기 값을 받아와서 플레이어 Object 움직임 조작
- 시간이 지남에 따라 점점 빠르게 내려오는 장애물 Object로 난이도 조절
- Firebase를 활용하여 DB에 랭킹 정보 저장, 불러오기

## Script로 보는 핵심 기능 📰

### 기울기 센서 값을 받아와서 플레이어를 좌, 우로 이동
```ruby
accelerometer = Input.acceleration.x * accelValue;
Physics2D.gravity = new Vector2(accelerometer, 1);

if (accelerometer > moveRight)
    playerRect.anchoredPosition += new Vector2(speedValue, 0.0f);
else if (accelerometer < moveLeft)
    playerRect.anchoredPosition -= new Vector2(speedValue, 0.0f);
```

기울기 값을 단말기에서 받아와서 플레이어 오브젝트의 위치 조정

### Object를 Instantiate, Destroy 하지 않고 위치 수정으로 무한히 재활용
```ruby
this.gameObject.GetComponent<RectTransform>().anchoredPosition = 
new Vector2(xPositionArray[random.Next(minValue, maxValue)], GetStartPosition().y);
```

내려오는 장애물을 새로 만들지 않고 시작지점의 Y 좌표와 랜덤한 X 좌표를 부여하여 Object 재활용

## Sample Image 🎮
<img src="https://github.com/user-attachments/assets/b0f04904-bc4b-4147-a706-d15fa8110560" width="270" height="480"/>  
<img src="https://github.com/user-attachments/assets/d4eaf867-003d-41bd-9e76-db81384a3116" width="270" height="480"/>
