using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecordNumberOfWins : SingletonMonoBehaviour<RecordNumberOfWins>
{
    public int CountWins { get; set; }
    public int CountLosses { get; set; }
}
