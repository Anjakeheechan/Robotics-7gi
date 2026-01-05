# MyFirstUnityProject - Unity 종합 학습 프로젝트

## 개요
이 프로젝트는 **Unity 엔진**을 활용한 종합 학습 및 데모 프로젝트입니다. **산업 자동화(로봇/PLC)**, **게임 개발**, **네트워킹**, **Firebase 백엔드**, 그리고 다양한 **Unity 기능 학습**을 위한 예제들이 포함되어 있습니다.

---

## 📁 프로젝트 구조

```
Assets/
├── Animations/          # 캐릭터 애니메이션 (Idle, Walk, Fireball 등)
├── AnimeGirls/          # 애니메 스타일 캐릭터 모델 (BaseCharacter, Casual1)
├── DownloadedAssets/    # 외부 에셋
│   ├── Free Furniture Set/
│   ├── Furniture_ges1/
│   └── WorrcoArts/
├── Firebase/            # Firebase SDK (Auth, Database)
├── IK_toolkit/          # 역운동학(IK) 시스템
│   ├── Models/          # UR_16e 로봇 모델
│   └── Scripts/         # IK 계산 스크립트
├── Materials/           # 머티리얼 및 물리 머티리얼
├── Plugins/             # 네이티브 플러그인
├── Prefabs/             # 프리팹
│   ├── Bullet, Cube, Player, Player2
│   ├── UR_16e Platform (산업용 로봇)
│   └── 금속, 플라스틱 (MPS용 워크피스)
├── Scenes/              # 씬 파일들 (26개 이상)
├── Scripts/             # C# 스크립트
│   └── MPSSimulator/    # MPS 시뮬레이션 전용 스크립트
├── Settings/            # URP 렌더 파이프라인 설정
├── Sounds/              # 오디오 파일 (BGM, SFX)
├── StreamingAssets/     # 런타임 데이터
└── TextMesh Pro/        # TMP 폰트 및 리소스
```

---

## 🎬 씬(Scene) 목록

### 🏭 산업 자동화
| 씬 이름 | 설명 |
|---------|------|
| **MPSSimulator.unity** | MPS 공정 시뮬레이션 (실린더, 컨베이어, 센서, PLC 연동) |
| **MoveSequence.unity** | 로봇 모션 시퀀스 티칭 및 실행 |
| **FloorPlan.unity** | 공장 레이아웃 시각화 |

### 🔥 Firebase 연동
| 씬 이름 | 설명 |
|---------|------|
| **FirebaseAuth.unity** | 이메일/비밀번호 인증 (회원가입, 로그인) |
| **FirebaseDB.unity** | 실시간 데이터베이스 CRUD 테스트 |

### 🌐 네트워킹
| 씬 이름 | 설명 |
|---------|------|
| **Muliplayer.unity** | TCP 소켓 기반 다중 클라이언트 동기화 |
| **MultiThreading.unity** | 멀티스레딩 테스트 |

### 🎮 게임 & 인터랙션
| 씬 이름 | 설명 |
|---------|------|
| **PlayerMove.unity** | 3인칭 캐릭터 이동 |
| **PlayerMove+Gun.unity** | 캐릭터 이동 + 총기 발사 |
| **UnityChan_FSM.unity** | FSM 기반 AI 캐릭터 동작 |
| **Minigame.unity** | 미니게임 예제 |
| **Pinball.unity** | 핀볼 게임 |
| **NavigationStudy.unity** | NavMesh 네비게이션 |

### 📚 학습용 예제
| 씬 이름 | 설명 |
|---------|------|
| **PhysicsSample.unity** | 물리 엔진 학습 (충돌, Rigidbody) |
| **RotationStudy.unity** | 회전 보간 학습 (Lerp, Slerp, Torque) |
| **CubeMove.unity** | 벡터 이동 알고리즘 (MoveTowards, Lerp) |
| **Clock.unity** | 시계 구현 (시/분/초 바늘 회전) |
| **UIStudy.unity** | UI 시스템 학습 |
| **AudioStudy.unity** | 오디오 시스템 학습 |
| **ParticleStudy.unity** | 파티클 시스템 학습 |
| **FileSaveLoad.unity** | 로컬 파일 입출력 |
| **Progress.unity** | 진행률 관리 시스템 |

---

## 🛠️ 주요 스크립트

### 로봇 & 자동화
| 스크립트 | 설명 |
|----------|------|
| `RobotManager.cs` | 로봇 수동 제어, 티칭, 시퀀스 실행 |
| `IK_toolkit.cs` | 역운동학 계산 (관절 각도 산출) |
| `MoveSequence.cs` | 다관절 로봇 순차 모션 제어 |

### MPS 시뮬레이션 (`Scripts/MPSSimulator/`)
| 스크립트 | 설명 |
|----------|------|
| `PLCManager.cs` | PLC 입출력 신호 중계 (Master/Slave 모드) |
| `MxComponent.cs` | 미쓰비시 PLC 통신 (MX Component) |
| `Cylinder.cs` | 공압 실린더 시뮬레이션 |
| `Conveyor.cs` | 컨베이어 벨트 동작 |
| `Sensor.cs` | 근접/금속 센서 |
| `TowerLamp.cs` | 3색 신호등 |
| `Loader.cs` | 자재 공급 장치 |

### Firebase
| 스크립트 | 설명 |
|----------|------|
| `FirebaseAuthManager.cs` | 이메일 인증, 로그인/로그아웃 |
| `FirebaseDBManager.cs` | 실시간 DB 읽기/쓰기 |
| `DBManager.cs` | 로컬 DB 관리 유틸리티 |

### 게임 메카닉
| 스크립트 | 설명 |
|----------|------|
| `TPSPlayerController.cs` | 3인칭 캐릭터 컨트롤러 |
| `PlayerMove.cs` | 기본 플레이어 이동 |
| `Gun.cs` | 총기 발사 로직 |
| `Bullet.cs` | 투사체 동작 |
| `FSMCharacter.cs` | 유한 상태 머신 AI |
| `ChaseTarget.cs` | 타겟 추적 AI |

### 유틸리티
| 스크립트 | 설명 |
|----------|------|
| `TCPClient.cs` | TCP 소켓 클라이언트 |
| `FileManager.cs` | 파일 저장/불러오기 |
| `AudioManager.cs` | 오디오 재생 관리 |
| `GameManager.cs` | 게임 상태 관리 |
| `UIManager.cs` | UI 이벤트 처리 |
| `Clock.cs` | 시계 로직 |

### 물리 & 수학 학습
| 스크립트 | 설명 |
|----------|------|
| `CubeMove.cs` | 벡터 이동 예제 |
| `SphereMove.cs` | 구체 물리 이동 |
| `LerpRotation.cs` | Lerp 회전 보간 |
| `SlerpRotation.cs` | Slerp 회전 보간 |
| `TorqueRotation.cs` | 토크 기반 회전 |
| `PhysicsStudy.cs` | 물리 학습 예제 |

---

## 🎨 에셋

### 3D 모델
- **Scara Robot** (`scara robot.fbx`) - 스카라 로봇 모델
- **UR_16e** (`IK_toolkit/`) - Universal Robots 협동 로봇
- **AnimeGirls** - 애니메 스타일 캐릭터
- **가구 세트** (`DownloadedAssets/`) - 인테리어 에셋

### 애니메이션
- Standing Idle, Walking, Fireball, Opening 등 캐릭터 애니메이션

### 오디오
- `bgm.wav` - 배경음악
- `punch1.wav` - 효과음

---

## 🚀 시작하기

### 요구 사항
- Unity 2021.3 LTS 이상
- Universal Render Pipeline (URP)
- Firebase SDK (인증/DB 기능 사용 시)
- MX Component (PLC 연동 시, 미쓰비시 전용)

### 실행 방법
1. Unity Hub에서 프로젝트를 엽니다.
2. 원하는 씬을 `Scenes/` 폴더에서 선택하여 엽니다.
3. **Play** 버튼을 눌러 실행합니다.

### 주요 테스트 시나리오

| 목적 | 씬 |
|------|-----|
| 로봇 제어 | `MoveSequence.unity` |
| MPS 시뮬레이션 | `MPSSimulator.unity` |
| Firebase 인증 | `FirebaseAuth.unity` |
| 게임 플레이 | `PlayerMove+Gun.unity` |
| 물리 학습 | `PhysicsSample.unity` |

---

## 📝 참고사항
- `google-services.json` 파일이 Firebase 연동에 필요합니다.
- PLC 연동(MxComponent)은 Windows 환경에서만 동작합니다.
- URP 설정은 `Settings/` 폴더에서 PC/Mobile별로 관리됩니다.
