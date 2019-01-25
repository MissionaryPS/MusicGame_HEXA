using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MainRoot {

    private Select2Play select;
    private Play2Result result;

    public void PassSelect(Select2Play data)
    {
        select = data;
    }

    public Select2Play GetSelect()
    {
        return select;
    }
    public void PassResult(Play2Result data)
    {
        result = data;
    }

    public Play2Result GetResult()
    {
        return result;
    }

}
