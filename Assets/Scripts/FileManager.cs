using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Rendering.LookDev;
using NUnit.Framework.Interfaces;

/// <summary>
/// 1. 객체를 JSON 파일(Serialization, 직렬화)로
/// 2. JSON 파일을 객체(Deserialization, 역직렬화)로,
/// 속성: 객체의 정보(클래스)
/// 기존파일: FileStream, StreamWriterm, StreamReader
/// * Newtonsoft.JSON 기능 패키지로 추가
/// </summary>
public class FileManager : MonoBehaviour
{
    // api 사용시 -> 환경변수로 등록
    public string openWeatherApiKey = "4a339009a7645f0ed92d1f61cca9f8f5";
    public string lat = "37.48"; // 소수점 둘째자리 까지 필요
    public string lon = "126.87";
    public string baseURL = "https://api.openweathermap.org/data/2.5/weather?";

    // OpenWeather API JSON -> 객체화
    public class Coord
    {
        public float lon;
        public float lat;
    }
    
    public class Weather
    {
        public int id;
        public string main;
        public string description;
        public string icon;
    }

    public class Main
    {
        public float temp, feels_like, temp_min, temp_max;
        public int pressure, humidity, sea_level, grnd_level;
    }

    public class Wind
    {
        public float speed;
        public int deg;
    }

    public class Clouds
    {
        public int all;
    }

    public class Sys
    {
        public int type, id;
        public string country;
        public long sunrise, sunset;
    }

    public class WeatherData
    {
        // JSON -> Object 만들때, key값 = 변수명 -> 기존 데이터의 Key 값을 변수명
        public Coord coord;
        public Weather[] weather;
        public string station;  // base -> station
        public Main main;
        public int visibility;
        public Wind wind;
        public Clouds clouds;
        public long dt;
        public Sys sys;
        public int timezone;
        public int id;
        public string name;
        public int cod;
    }

    public class Result
    {
        public class Name
        {
            public string title;
            public string first;
            public string last;
        }
        public class Location
        {
            public class Street
            {
                public int number;
                public string name;
            }
            public class Coordinates
            {
                public string latitude;
                public string longitude;
            }
            public class Timezone
            {
                public string offset;
                public string description;
            }

            public Street street;
            public string city;
            public string state;
            public string country;
            public string postcode;
            public Coordinates coordinates;
            public Timezone timezone;
        }
        public class Login
        {
            public string uuid;
            public string username;
            public string password;
            public string salt;
            public string md5;
            public string sha1;
            public string sha256;
        }
        public class Dob
        {
            public string date;
            public int age;
        }
        public class Registered
        {
            public string date;
            public int age;
        }
        public class Id
        {
            public string name;
            public string value;
        }
        public class Picture
        {
            public string large;
            public string medium;
            public string thumbnail;
        }

        public string gender;
        public Name name;
        public Location location;
        public string email;
        public Login login;
        public Dob dob;
        public Registered registered;
        public string phone;
        public string cell;
        public Id id;
        public Picture picture;
        public string nat;
    }
    public class Info
    {
        public string seed;
        public int results;
        public int page;
        public string version;
    }

    public class RandomUser
    {
        public Result[] results;
        public Info info;
    }

    public RandomUser user;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 1. Serialization: Object -> JSON
        Sys sys = new Sys();
        sys.country = "South Korea";
        sys.id = 0;
        sys.type = 1;
        sys.sunset = 1111111;
        sys.sunrise = 2222222;

        string json = JsonUtility.ToJson(sys);
        print(json);

        // 2. Deserialization: JSON -> Object
        Sys sys2 = JsonUtility.FromJson<Sys>(json);
        print($"{sys2.country}/{sys2.id}/{sys2.type}/{sys2.sunset}/{sys2.sunrise}");

        StartCoroutine(CoGetWeatherData("https://randomuser.me/api/"));
    }

    IEnumerator CoGetWeatherData()
    {
        string totalURL = baseURL + $"lat={lat}&lon={lon}&appid={openWeatherApiKey}";

        UnityWebRequest www = UnityWebRequest.Get(totalURL);

        // 웹서버에 데이터 요청을 호출
        yield return www.SendWebRequest();

        string json = www.downloadHandler.text;
        Debug.Log(json);

        json = json.Replace("base", "station");

        WeatherData data = JsonConvert.DeserializeObject<WeatherData>(json);
        Debug.Log(data.sys.country);
    }

    IEnumerator CoGetWeatherData(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);

        // 웹서버에 데이터 요청을 호출
        yield return www.SendWebRequest();

        string json = www.downloadHandler.text;
        Debug.Log(json);

        RandomUser data = JsonConvert.DeserializeObject<RandomUser>(json);
        Debug.Log($"{data.results[0].name.first} {data.results[0].name.last}");

        Result.Location newLocation = new Result.Location();
        newLocation.state = "";
        newLocation.street = new Result.Location.Street();
        newLocation.street.number = 568;
        newLocation.street.name = "가산디지털로";
        newLocation.city = "서울시";
        newLocation.country = "대한민국";
        newLocation.postcode = "46546";
        newLocation.coordinates = new Result.Location.Coordinates();
        newLocation.coordinates.latitude = "37.33";
        newLocation.coordinates.longitude = "127.33";

        ChangeAddress(ref data, newLocation);
    }

    public void ChangeAddress(ref RandomUser user, Result.Location newLocation)
    {
        if (user == null)
        {
            Debug.LogWarning("User 정보가 없습니다.");
            return;
        }

        user.results[0].location = newLocation;
        string json = JsonConvert.SerializeObject(user);
        Debug.Log("주소가 변경되었습니다.");
        Debug.Log(json);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
