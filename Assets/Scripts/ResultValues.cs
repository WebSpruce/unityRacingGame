using System;

[Serializable]
public class ResultValues
{
    public string result;
    public string date;

    public ResultValues(string result, DateTime date)
    {
        this.result = result;
        this.date = date.ToString("dd MMMM yyyy HH:mm:ss");
    }
}
