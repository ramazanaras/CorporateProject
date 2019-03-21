using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using System.Transactions;//referanslara ekledik

namespace CorporateProject.Core.Aspects.Postsharp.TransactionAspects
{
    [Serializable]
    public class TransactionScopeAspect:OnMethodBoundaryAspect
    {
        private TransactionScopeOption _option;

        public TransactionScopeAspect()
        {
                
        }
        public TransactionScopeAspect(TransactionScopeOption option)
        {
            _option = option;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
          args.MethodExecutionTag=new TransactionScope(_option);
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            //metot başarılı olursa (try içinde )
           ((TransactionScope) args.MethodExecutionTag).Complete();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            //metottan çıktığımızda(catch kısmında)
            ((TransactionScope) args.MethodExecutionTag).Dispose();
        }
    }
}
