# DokiDokiNBC
Unity 숙련 주차 팀 과제
## 🙌 안녕하세요. DokiDokiNBC를 만든 **육감으로 코딩하조**입니다!
<img src="https://github.com/JY-LemongO/DokiDokiNBC/assets/54103715/5a2441d1-a4b5-41a6-82de-7766f911b068" width="500" height="500">

## 프로젝트 소개
악몽 속에서 12시간 캠프 일정을 소화하며 코드 조각을 모으고, 퇴실 버튼을 찾아 던전을 클리어 해 나가자!

### 멤버구성 및 역할분담
+ 팀장 국지윤 :

    ★★★ : 아이템 수집 

    ★★ : 피해와 체력 관리
  
    ★★ : 퍼마데스
  
+ 팀원 김준하 :

    ★★★ : 전투 시스템

  
    ★★ : 캐릭터조작
  
+ 팀원 정원우 :

    ★★★ : 몬스터 생성 및 AI
  
    ★★★ : 보스 생성
  
+ 팀원 최재성 :

    ★★★ : 랜덤 던전 생성

    ★★ : 아티팩트 및 효과

# 기능 설명

## 필수 요구

1. **랜덤 던전 생성** (난이도: ★★★☆☆)

<details>
  <summary>랜덤 던전 생성</summary>

<div>
랜덤한 위치에 절차적으로 생성된 던전 맵을 만들기 위해 던전을 구성하는 방의 위치정보를 먼저 구한뒤에 맵 생성을 진행하였습니다.</br>
던전 방의 생성 위치의 경우 상하좌우 중 랜덤한 방향으로 생성할 던전 방의 정보에 들어있는 가로 세로의 크기를 사용해 다음 방의 위치를 계산했으며,</br>
생성된 좌표는 만들어 놓은 리스트에 담아 이미 생성된 좌표인지 확인을 하고 이미 만들어진 좌표라면 해당 좌표에 다시 상하좌우중 한 방향으로 해당 좌표를 다시 계산 하도록 했습니다.</br>
</div>

<details>
  <summary>코드</summary>
  <div>
    <pre>
      <code>
    public List<Vector2Int> RandomCreateRoomPosition(Vector2Int startPoint,int maxRoomCount)
   {
    List<Vector2Int> path = new List<Vector2Int>();

    Vector2Int curPoint = startPoint;
    // -- Init
    path.Add(curPoint);
    RoomData curRoomData = new RoomData(roomDataSOs[Random.Range(0,roomDataSOs.Count)]);
    curRoomData.SetRoomData(curPoint,0);
    curRoomData.clear = true;
    DunGoenManager.Instance.dungoenRoomDataList.Add(curRoomData);
    // -- Init
    for (int i = 1; i < maxRoomCount; i++)
    {
        curRoomData = new RoomData(roomDataSOs[Random.Range(0, roomDataSOs.Count)]);
        curPoint += GetCreatePoint(curRoomData);

        while (path.Contains(curPoint))
        {
            curPoint += GetCreatePoint(curRoomData);
        }
        path.Add(curPoint);
        curRoomData.SetRoomData(curPoint,i);
        
        DunGoenManager.Instance.dungoenRoomDataList.Add(curRoomData);
    }
    return path;
}
 Vector2Int GetCreatePoint(RoomData roomData)
 {
     Vector2Int ranDir = dir[Random.Range(0, dir.Count)];
     ranDir *= new Vector2Int(roomData.width+offset,roomData.height+offset);
     return ranDir;
 }
      </code>
    </pre>
  </div>
</details>

  위에서 구한 생성할 던전 방의 위치정보를 사용해 타일 맵으로 맵을 그려줍니다.  </br>
  위치정보를 구할때 방을 구성하는 데이터를 가진 룸 데이터에 생성위치와, 생성번호를 사용해 기본적인 데이터를 초기화 하도록 했습니다.</br>
  <details>
    <summary>코드</summary>
    <h4>RoomData</h4>
    <pre>
      <code>
    public void SetRoomData(Vector2Int createPoint,int roomNumber)
    {
     center = createPoint;
     this.roomNumber = roomNumber;
     bounds = new BoundsInt(new Vector3Int(center.x - (width / 2), center.y - (height / 2), 0), new Vector3Int(width, height, 0));
     minCamLimit = new Vector2(center.x - width / 2, center.y - height / 2) + new Vector2(DunGoenManager.Instance.cameraWidth, DunGoenManager.Instance.cameraHeight);
     maxCamLimit = new Vector2(center.x + width / 2, center.y + height / 2) - new Vector2(DunGoenManager.Instance.cameraWidth, DunGoenManager.Instance.cameraHeight);
     leftDoorPoint = new Vector2Int(center.x - (width / 2), center.y);
     rightDoorPoint = new Vector2Int(center.x + (width / 2), center.y);
     topDoorPoint = new Vector2Int(center.x, center.y + (height / 2));
     bottomDoorPoint = new Vector2Int(center.x, center.y - (height / 2));
     }
      </code>
    </pre>
  </details>
  BoundsInt 의 경우 정수형으로 구성된 경계 상자를 나타내는데 좌표 공간 내에서 경계 상자의 위치와 크기를 쉽게 가져올 수있습니다. </br>
  그래서 이를 이용해서 타일맵을 그려줄 것입니다.</br>
  <details>
    <summary>코드</summary>
    <h4>TileDraw</h4>
    <pre>
      <code>
      void DrawTile(Tilemap tilemap,Vector2Int drawPoint,TileBase tile)
     {
     Vector3Int position = tilemap.WorldToCell((Vector3Int)drawPoint);
     tilemap.SetTile(position, tile);
     }
      </code>
    </pre>
  </details>

<p>
  <img src="https://github.com/hygge31/MatchUp/assets/121877159/802657cd-5b4f-4a75-a98c-6d700c8183b8" width="350px" />
</p>

 
<h3>2. 포탈 만들기</h3>
만들어진 던전 방에서 랜덤으로 하나의 방에만 포탈 생성, 방에있는 몬스터가 모두 죽었을 경우에만 포탈 활성화됩니다.</br>
포탈을 사용하게 되면 메인 씬으로 이동</br>

<p>
  <img src="https://github.com/hygge31/MatchUp/assets/121877159/78e07cc6-7797-43e0-9d5f-94ae5e9f96bb" width="350px" />
</p>

<h3>3. 스폰 만들기</h3>
스포너 클래스를 만들어 던전 방에 랜덤한 위치에 스포너를 위치시키고 랜덤한 몬스터를 소환.</br>
해당 방에 존재하는 몬스터가 죽었을때 해당 방에 클리어 조건 카운트.</br>
클리어 조건을 충족하면 이동가능한 문 등장.</br>

<p>
  <img src="https://github.com/hygge31/MatchUp/assets/121877159/36173bbb-75ed-4913-ab7e-d8a513ce1997" width="350px" />
</p>

<h3>4. 보스룸 만들기</h3>
4번째 날일 경우 다음 스테이지에 보스방 등장.</br>
보스몬스터의 체력을 확인하고 체력이 감소했을경우 보스몬스터의 체력을 보여주는 UI 업데이트</br>



</details>


2. **캐릭터 조작** (난이도: ★★☆☆☆)
    
    - 설명
      
3. **아이템 수집** (난이도: ★★★☆☆)
    
   - 아이템 종류
      - Perk : 플레이어 능력치 보정
        - 공격력 증가
        - 공격 속도 증가
        - 이동 속도 증가 (중첩 불가)
        - 투사체 개수 증가
        - 관통 수 증가
      - Weapon : 공격 로직 변경
        - 파이어볼
        - 미사일 포드
        - 낙뢰
      
4. **몬스터 생성 및 AI** (난이도: ★★★☆☆)
    
    - 몬스터 2 종류
      
        - 몬스터 1 : 버섯 (근접)

            속도가 약간 빠르고 체력이 약하다.

            ![화면 캡처 2024-02-16 033924](https://github.com/JY-LemongO/DokiDokiNBC/assets/54103715/55598ea4-f66a-4693-bd50-993a0c16b313)

        - 몬스터 2 : 사신 (근접)

            속도가 느리고 체력이 많다.

            ![화면 캡처 2024-02-16 033903](https://github.com/JY-LemongO/DokiDokiNBC/assets/54103715/eb38a7fc-3d7d-427d-84ef-d2ee25aee636)

    - 전투 방식
  
        - 플레이어를 쫒아가면서 공격 가능 범위에 들어오면 멈춰서 공격을 한다.
     
    - 애니메이션
  
        - 기본 상태 , 걷기 , 공격 , 피격 , 사후 처리로 구성되어 있다.
     
    - 오디오 재생
  
        - 공격할 때 , 죽을 때 오디오를 재생한다. 
          
    - 보상
  
        - 몬스터가 죽으면 플레이어에게 도움이 되는 아이템을 확률적으로 떨어뜨린다. 
      
5. **전투 시스템** (난이도: ★★★☆☆)
    
    - 설명
      
6. **피해와 체력 관리** (난이도: ★★☆☆☆)
    
    - 플레이어는 어떤 공격을 받아도 1의 데미지만 받는다.
    - 데미지를 받을 때 Action 을 이용한 HP UI 연동 및 Death UI 생성
  
    - ![1](https://github.com/JY-LemongO/DokiDokiNBC/assets/122505119/9553b7f1-0d1c-4c37-9517-5129f866e7a9)
    - 회복 아이템을 먹으면 HP 증가 및 UI
      
7. **보스 전투** (난이도: ★★★☆☆)

    - 난이도
        
       보스는 체력에 비례하여 총 4단계로 나뉘어져 있다.
  
        - 1단계 : 70% 이상 패턴 1번 2번 중에서 랜덤으로 하나 발사.
     
        - 2단계 : 40% 이상 70% 미만 1단계 3번 패턴을 쏘고 1단계를 실행. 
     
        - 3단계 : 20% 이상 40% 미만 4번 5번 중에서 하나를 쏘고 2단계를 실행.
     
        - 4단계 : 20% 미만 3번 패턴을 한 번 쏘고 3단계를 실행.

    - 이동 처리
  
        - 보스가 바라보는 방향으로 등속도로 날아간다
        
        - 벽과 물체에 닿으면 입사각 반사각으로 튕겨나간다.
     
        - 5초마다 이동방향을 랜덤한 방향으로 바꾼다.  
   
    - 보스 공격 패턴
      
        - 패턴 1 : 
     
          보스가 바라보는 곳으로 총알을 한 개 발사한다.
          
          ![화면 캡처 2024-02-16 031254](https://github.com/JY-LemongO/DokiDokiNBC/assets/54103715/3326764c-92ab-4cad-963d-67ce947418ea)
          
        - 패턴 2 : 
          
          보스가 바라보는 곳으로 총알을 다섯 개 발사한다.
  
          ![화면 캡처 2024-02-16 031631](https://github.com/JY-LemongO/DokiDokiNBC/assets/54103715/a331679f-ddc6-44f3-8f3f-7864357a71cf)

        - 패턴 3 : 
     
          보스에서 360도로 뻗어 나가는 총알 발사

          ![화면 캡처 2024-02-16 032318](https://github.com/JY-LemongO/DokiDokiNBC/assets/54103715/9fd14833-4754-4b59-8807-fe877c94edf6)

        - 패턴 4 :
     
          보스가 총알 두 개를 앞쪽으로 발사하고 2초 뒤에 패턴 3모양처럼 퍼진다.

          ![화면 캡처 2024-02-16 033026](https://github.com/JY-LemongO/DokiDokiNBC/assets/54103715/947faa02-854b-4649-94a7-83fd27a78fe2)

        - 패턴 5 :
     
          보스가 플레이어를 몇 초간 추적하는 총알 다섯 개를 발사한다.

          ![화면 캡처 2024-02-16 032523](https://github.com/JY-LemongO/DokiDokiNBC/assets/54103715/56ce9243-0bdb-40e3-a354-d974bc2fe07b)
        
7. **퍼마데스 (Permadeath) 시스템** (난이도: ★★☆☆☆)
    
    - PlayerStatManager 와 AttakManager에서 각각 플레이어의 Perk, Stat 정보 및 장착 무기 정보를 Init 함수를 통해 초기 상태로 돌린다.
    - 사망 시 GameManager의 진행도를 의미하는 Day 역시 초기값 1 로 설정하여 MainScene으로 씬 전환하여 퍼마데스 구현.
      
8. **아티팩트 및 효과** (난이도: ★★☆☆☆)
    
    - 게임 플레이에 영향을 미치는 아티팩트나 효과를 추가합니다.

## 선택 요구

1. **캐릭터 특성 및 레벨업 시스템** (난이도: ★★★☆☆)
    
    - 설명
      
2. **스토리 요소** (난이도: ★★☆☆☆)
    
    - 현실에서 12시간 꿈속에서 12시간 내배켐과 벗어날 수 없는 현실이 찾아온다!!
      
3. **사운드 및 음악** (난이도: ★☆☆☆☆)
    
    - 게임에 사운드 효과와 음악을 추가하여 게임의 분위기를 개선합니다.

## 게임 줄거리

내일배움캠프를 수강하는 주인공은 수료식까지 4일만을 남기고 힘든 일과를 마친다. 

잠을 청하는 주인공.. 

눈을 뜬 곳은 평소와는 다른 9 to 9을 보내는 내배켐이었다. 

그렇게 힘든 하루 일과를 마친 주인공은 눈이 감겨오는데..

놀랍게도 방금 보낸 일과는 꿈속이었다..

<img src="https://github.com/JY-LemongO/DokiDokiNBC/assets/54103715/6094077e-4e7a-44e4-95ad-a1837e4fdf18" width="400" height="400">

말그대로 9 to 9 이었다.

계속해서 현실과 꿈속에서 9 to 9을 보내는 주인공!

주인공은 무사히 수료할 수 있을 것인가!

