using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Templates : MonoBehaviour
{
    //----BaseResponse----
    [Serializable]
    public class BaseResponse
    {
        public string id = "";
        public string name = "";
        public string response = "";
    }

    //----PING----
    [Serializable]
    public class REQUES_PING
    {
        public string id = "";
        public string type = "GET";
        public string name = "PING";
        public string body = "";
    }

    [Serializable]
    public class RESPONSE_PING{
        public int code;
        public string body;
    }

    //----VERSION----
    [Serializable]
    public class REQUES_VERSION
    {
        public string id = "";
        public string type = "GET";
        public string name = "VERSION";
        public string body = "";
    }

    [Serializable]
    public class RESPONSE_VERSION
    {
        public int code;
        public Response_VERSION_level2 body;
    }

    [Serializable]
    public class Response_VERSION_level2
    {
        public string server;
        public string api;
    }

    //----GETAPI----
    [Serializable]
    public class REQUES_GETAPI
    {
        public string id = "";
        public string type = "GET";
        public string name = "GETAPI";
        public string body = "";
    }

    [Serializable]
    public class RESPONSE_GETAPI
    {
        public int code;
        public string body;
    }

    //----REG----
    [Serializable]
    public class REQUES_REG
    {
        public string id = "";
        public string type = "POST";
        public string name = "REG";
        public string body = "nik_name";
    }

    [Serializable]
    public class RESPONSE_REG
    {
        public int code;
        public RESPONSE_REG_level2 body;
    }

    [Serializable]
    public class RESPONSE_REG_level2
    {
        public string name;
    }

    //----CREATELOBBY----

    [Serializable]
    public class REQUES_CREATELOBBY
    {
        public string id = "";
        public string type = "POST";
        public string name = "CREATELOBBY";
        public string body = "";
    }

    [Serializable]
    public class RESPONSE_CREATELOBBY
    {
        public int code;
        public string body;
    }

    //----GETLOBBYCODE----

    [Serializable]
    public class REQUES_GETLOBBYCODE
    {
        public string id = "";
        public string type = "GET";
        public string name = "GETLOBBYCODE";
        public string body = "";
    }

    [Serializable]
    public class RESPONSE_GETLOBBYCODE
    {
        public int code;
        public string body;
    }

    //----JOINLOBBY----

    [Serializable]
    public class REQUES_JOINLOBBY
    {
        public string id = "";
        public string type = "POST";
        public string name = "JOINTOLOBBY";
        public string body = "lobby_id";
    }

    [Serializable]
    public class RESPONSE_JOINLOBBY
    {
        public int code;
        public string body;
    }

    //----GETPLAYERS----

    [Serializable]
    public class REQUES_GETPLAYERS
    {
        public string id = "";
        public string type = "GET";
        public string name = "GETPLAYERS";
        public string body = "";
    }

    [Serializable]
    public class RESPONSE_GETPLAYERS
    {
        public int code;
        public List<string> body;
    }

    //----UPDATEPLAYER----

    [Serializable]
    public class REQUES_UPDATEPLAYER
    {
        public string id = "";
        public string type = "POST";
        public string name = "UPDATEPLAYER";
        public string body = "";
    }

    [Serializable]
    public class REQUES_UPDATEPLAYER_level2
    {
        public string id = "";
        public REQUES_UPDATEPLAYER_level3 body;
    }

    [Serializable]
    public class REQUES_UPDATEPLAYER_level3
    {
        public float xPos;
        public float zPos;
        public float BaseYRot;
        public float TowerYRot;
        public float xVel;
        public float zVel;
    }

    [Serializable]
    public class RESPONSE_UPDATEPLAYER
    {
        public int code;
        public string body;
    }
}
