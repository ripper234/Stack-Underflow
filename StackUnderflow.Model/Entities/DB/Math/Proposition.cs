using System.Collections.Generic;
using Castle.ActiveRecord;

namespace StackUnderflow.Model.Entities.DB.Math
{
    [ActiveRecord]
    public class Proposition
    {
        [HasMany]
        public List<Proposition> Assumptions { get; set; }

        [HasMany]
        public List<Proposition> Consequences { get; set; }

        [Property]
        public PropositionType Type { get; set; }
    }

    public enum PropositionType
    {
        Axiom,
        Proposition,
        Step,
        Lemma,
    }
}
