using System.Collections.Generic;

[System.Serializable]
public class DadosSave
{
    public string nomeFase;

    public bool checkpointAtivado;

    public int moedasNoCheckpoint;

    public List<string> moedasColetadasCheckpoint;
}