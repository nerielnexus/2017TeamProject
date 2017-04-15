# ActionGauge
## 적용하는 방법

    1. ActionGauge.unitypackage 를 임포트한다.
    2. Resource/Script 의 HouseControl 를 집 prefab 에 붙인다.
    3. 플레이어에게 MyPlayer 태그를 붙여준다.
    4. 게임을 실행한 후 플레이어를 집에 가까이 붙인 후 스페이스를 눌러 게이지를 활성화한다.
    5. 게이지가 생겨서 움직이면 스페이스를 눌러서 성공 혹은 실패를 한다.

## 내용물 설명

- Resources 폴더

    게이지의 파일들이 들어있는 폴더

- Resource/Image 폴더

    게이지에 사용된 이미지들을 모은 폴더

- Resource/Script 폴더

    게이지에 사용된 스크립트를 모은 폴더

    - HouseControl.cs
    
            집 prefab 에 붙이는 스크립트.
            NumberChange.cs 를 호출하는 등 액션 게이지의 핵심 스크립트이다.

    - NumberChange.cs
    
            액션게이지의 게이지 바에 붙어있는 스크립트.
            게이지 바가 움직이며 바의 위치에 따라 성공과 실패를 판정한다.

- Prefab 목록

    - ActionGauge

        액션게이지이다.

    - NotifySuccess, NotifyFailure
    
        게이지를 빨간 영역에 맞춰서 스페이스를 눌렀을때 성공과 실패을 알려준다.