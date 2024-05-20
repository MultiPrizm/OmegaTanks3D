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
        public string body = "";
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
}
