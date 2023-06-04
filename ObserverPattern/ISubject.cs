using System;
using System.Collections.Generic;

namespace ObserverPattern
{
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }


}
