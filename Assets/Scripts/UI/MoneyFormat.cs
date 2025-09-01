using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoneyFormat
{
    public static string Thounsand(double money) => money.ToString("0,0");
}
