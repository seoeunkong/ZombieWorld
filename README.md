# ZombieWorld
뱀서라이크 게임 

 
## 프로젝트 소개
- 제한 시간동안 몬스터를 무기와 아이템을 이용해 처치하고 HP가 0이 되지 않으면 승리합니다. 
- 원거리 공격, 근접 공격, 발사형 공격 등 3가지의 무기를 이용해 몬스터 처치할 수 있습니다. 
- 몬스터를 죽이면 코인 혹은 자석을 드랍함. 코인을 획득하면 플레이어의 경험치가 쌓입니다. 
- 각 레벨에 맞는 경험치 획득하면 무기 및 아이템 업그레이드 가능하고, 이를 통해 플레이어 이동 속도나 무기 데미지, 발사 속도를 증가할 수 있습니다.



 
## 개발 환경
- 개인 프로젝트 
- 엔진 : Unity
- 버전 및 이슈관리 : Github

 
## 개발 기간
2023.10.11 ~ 2023.10.25

 
## 프로젝트 구조 
- 인벤토리에 담길 아이템 
  ![BaseObject 관련 다이어그램~](https://github.com/seoeunkong/WizardingWorld/assets/87869785/fbd1ee4e-ab59-42d1-bf7e-9071ebcdee21)


- 인벤토리에 담길 아이템 데이터
  ![ObjectData 관련 다이어그램](https://github.com/seoeunkong/WizardingWorld/assets/87869785/be33e9fd-962f-4f0f-944d-eebcda9c8a53)


- FSM 구현을 위한 상태 패턴
  ![basestate 관련 다이어그램](https://github.com/seoeunkong/WizardingWorld/assets/87869785/ccc65e33-f63c-4bf0-bbb5-f3407c9400d0)


- 플레이어와 몬스터 행동 관리
  ![CharacterController 다이어그램](https://github.com/seoeunkong/WizardingWorld/assets/87869785/c480c57b-8d95-4ac4-8b93-016b328171a7)


