using System;
using System.Collections.Generic;

namespace ObserverPattern
{
    public interface ISubject
    {
        void Attach(IObservable observer);
        void Detach(IObservable observer);
        void Notify();
    }


}
