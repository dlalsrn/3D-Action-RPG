# 3D Action RPG
![image](https://github.com/user-attachments/assets/dc55393e-f018-4466-b2a4-54a25ea356c9)
<br><br/>

## 프로젝트 개요
- **프로젝트 이름**: 3D Action RPG
- **프로젝트 기간**: 2025.06.14 - 2025.06.21
- **팀 구성**: 개인 프로젝트
<br><br/>

## 프로젝트 목표
Unity3D 환경에서 적 AI와 전투 시스템, 카메라 연출, 캐릭터 이동 및 상태 관리 등 3D Action RPG 게임을 직접 설계 및 구현해보는 것을 목표로 함.

FSM, NavMesh, Ragdoll, Cinemachine 등 다양한 기능을 사용하여 개발 역량을 쌓는데 중점을 두었음.
<br><br/>

## 플랫폼 및 기술 스택
- **게임 엔진**: Unity 6000.0.47f1
- **프로그래밍 언어**: C#
- **플랫폼**: PC
<br><br/>

## 게임 목표
필드에 있는 모든 적을 처치하는 것이 게임의 목표.
<br><br/>

## 게임 주요 기능
- **StateMachine 기반 캐릭터/적**
  
- **NavMeshAgent를 통한 AI 이동 및 추적 로직**
  
- **Player 콤보 공격 시스템**
  
- **Cinemachine을 활용한 FreeLook/Target 카메라 시스템**
  
- **Enemy 및 Player 체력바 UI - 상황별 활성화/숨김 관리**
<br><br/>

## 사용한 Asset
- **Paladin J Nordstrom**: https://www.mixamo.com
  - Player Character
    
- **minotaur1**: https://assetstore.unity.com/packages/3d/characters/minotaur1-196863
  - Enemy Character
    
- **Viking Village URP**: https://assetstore.unity.com/packages/essentials/tutorial-projects/viking-village-urp-29140
  - 맵 Asset
    
- **Font**: https://source.typekit.com/source-han-serif/kr/#get-the-fonts

사용한 Asset File들은 저작권 관련 문제가 발생하는 것을 방지하기 위해 업로드에서 제외함.

필요한 에셋은 위에 제공된 링크에서 개별적으로 다운로드 받아 추가해야함.
<br><br/>

## 개발 과정
https://velog.io/@lmg0052/series/Unity3D-3D-Action-RPG
<br><br/>

## 트러블 슈팅 & 개선 경험 요약
- **Cinemachine 3.x.x 적용**
  - 최신 버전의 Cinemachine에서 Target/LookAt이 통합되어 구글 검색 및 정보 부족으로 시행착오를 겪음.
  - 여러 설정을 실험하며 Tracking Target 및 Binding Mode를 최적화하여 자연스러운 카메라 무빙을 구현.
    
- **FSM-Animator 불일치 문제**
  - 다양한 State 전환에서 FSM과 Animator가 어긋나 캐릭터가 비정상적으로 동작하는 버그가 발생.
  - 코드 흐름, Animation Event 타이밍을 반복 조정하여 정상적으로 동작하도록 개선.
    
- **공격/피격 동시 처리 버그**
   - 공격 중 피격될 때 State와 Weapon Collider 활성화가 꼬여서 Collider가 비정상적으로 작동.
   - Attack State에서만 Collider가 활성화되도록 조건을 추가해 문제를 해결.
     
- **중력과 Character Controller의 isGrounded 판정 오류**
  - CharacterController의 isGrounded가 Move() 호출 이후에만 정확하게 갱신된다는 점을 발견.
  - 코드 실행 순서를 조정해 중력 처리 버그를 해결.
    
- **Ragdoll 적용 시 모델 일그러짐**
  - Enemy에 Ragdoll 추가 시 NavMeshAgent, Animator, CharacterController를 비활성화하지 않아 물리 충돌이 꼬이는 현상 발생.
  - Die State에서 관련 컴포넌트를 비활성화하여 자연스러운 Ragdoll 효과를 구현.
    
## 스크린샷 및 게임 플레이 영상
![image](https://github.com/user-attachments/assets/2fb5d9f9-4a47-4751-89d2-5ba592eb2dd4)
![image](https://github.com/user-attachments/assets/400d86a8-2c81-463c-90cf-35bf77ae2f22)
![Honeycam+2025-06-21+19-52-55 (1)](https://github.com/user-attachments/assets/829ea606-1f6a-4518-b942-adccf73d1860)

<br><br/>

- **게임 플레이 영상 링크**: https://youtu.be/VYAuyTwwTZk
<br><br/>

## 연락처
- **E-Mail**: qazzaq1541@gmail.com
