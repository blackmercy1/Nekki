using UnityEngine;

namespace Game.Common.Filters
{
    public class MemberFilter : FilterDecorator
    {
        private readonly Team _teamMember;
        private readonly bool _differenceFlag;

        public MemberFilter() : base()
        {
        
        }
    
        public MemberFilter(
            IFilter child,
            Team teamMember,
            bool differenceFlag) 
            : base(child)
        {
            _teamMember = teamMember;
            _differenceFlag = differenceFlag;
        }
    
        protected override bool CheckInternal(GameObject obj)
        {
            if (_differenceFlag)
            {
                if (obj.TryGetComponent<IMember>(out var differentTeam))
                    return _teamMember != differentTeam.TeamMember && Child.Check(obj);
            }
        
            else 
            if (obj.TryGetComponent<IMember>(out var teamMember))
                return _teamMember == teamMember.TeamMember && Child.Check(obj);
            return default;
        }
    }

    public interface IMember
    {
        Team TeamMember { get; }
    }
}