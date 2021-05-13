﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace CustomersCanvasSample.Services
{
    public class UIFrameworkService: IDisposable
    {
        private JsonDocument _config;

        public UIFrameworkService(JsonDocument config)
        {
            _config = config;
        }

        public string GetConfigAsJsonString(bool prettyPrint)
        {
            using var stream = new MemoryStream();
            using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { 
                Indented = prettyPrint, 
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping  // Otherwise it will ruin quotes in the config 
            }))
            {
                _config.WriteTo(writer);
            }

            return System.Text.Encoding.UTF8.GetString(stream.ToArray());
        }

        public JsonDocument Config { get { return _config; } }

        static public UIFrameworkService FromAppData(string configFileName)
        {
            using (StreamReader r = new StreamReader(configFileName))
            {
                return new UIFrameworkService(JsonDocument.Parse(r.BaseStream));
            }
        }

        public void Dispose()
        {
            _config.Dispose();
            _config = null;
        }
    }
}
