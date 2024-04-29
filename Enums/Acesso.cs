using System.Text.Json.Serialization;

namespace UsersManager.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Acesso
{
    COMUM,
    ADMIN
}