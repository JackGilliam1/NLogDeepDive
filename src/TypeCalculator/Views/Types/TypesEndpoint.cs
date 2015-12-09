using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;
using NLog;
using TypeCalculator.Core;

namespace TypeCalculator.Views.Types
{
    public class TypesEndpoint
    {
        private readonly Logger _logger = LogManager.GetLogger("TypesEndpoint");
        private readonly ITypesDictionary _typesDictionary;

        public TypesEndpoint(ITypesDictionary typesDictionary)
        {
            _typesDictionary = typesDictionary;
        }

        [UrlPattern("types/getStats")]
        public TypesResponse GetStats(TypesRequest request)
        {
            if (request.ElementTwo == ElementType.None || request.ElementOne == request.ElementTwo)
            {
                _logger.Debug("Getting Stats For {0}", request.ElementOne);
                return GetStatsFor(request.ElementOne);
            }
            if (request.ElementOne == ElementType.None)
            {
                _logger.Debug("Getting Stats For {0}", request.ElementTwo);
                return GetStatsFor(request.ElementTwo);
            }

            _logger.Debug("Getting Stats For {0} and {1}", request.ElementOne, request.ElementTwo);
            var typeOne = request.ElementOne;
            var typeTwo = request.ElementTwo;
            var strongDefense = _typesDictionary.GetStrongDefense(typeOne)
                .Concat(_typesDictionary.GetStrongDefense(typeTwo))
                .GroupBy(x => x)
                .ToList();
            var weakDefense = _typesDictionary.GetWeakDefense(typeOne)
                .Concat(_typesDictionary.GetWeakDefense(typeTwo))
                .GroupBy(x => x)
                .ToList();

            var commonElements = strongDefense.Where(y => weakDefense.Any() && weakDefense.Any(x => x.Key == y.Key))
                .Select(x => x.Key)
                .ToList();

            var immuneDefense = _typesDictionary.GetImmuneDefense(typeOne)
                .Concat(_typesDictionary.GetImmuneDefense(typeTwo))
                .Distinct();

            weakDefense = weakDefense.Where(x => !commonElements.Contains(x.Key) && !immuneDefense.Contains(x.Key)).ToList();

            strongDefense = strongDefense.Where(x => !commonElements.Contains(x.Key) && !immuneDefense.Contains(x.Key)).ToList();

            return new TypesResponse
            {
                StrongAttack = GetMultiTypeAttackStats(typeOne, typeTwo, _typesDictionary.GetStrongAttack),
                WeakAttack = GetMultiTypeAttackStats(typeOne, typeTwo, _typesDictionary.GetWeakAttack),
                StrongDefense = GetMultiTypeStats(strongDefense),
                WeakDefense = GetMultiTypeStats(weakDefense),
                ImmuneDefense = immuneDefense.Select(x => x.ToString()).ToList()
            };
        }

        private TypesResponse GetStatsFor(ElementType type)
        {
            return new TypesResponse
            {
                StrongAttack = _typesDictionary.GetStrongAttack(type).Select(x => x.ToString()),
                WeakDefense = _typesDictionary.GetWeakDefense(type).Select(x => x.ToString()),
                ImmuneDefense = _typesDictionary.GetImmuneDefense(type).Select(x => x.ToString()),
                StrongDefense = _typesDictionary.GetStrongDefense(type).Select(x => x.ToString()),
                WeakAttack = _typesDictionary.GetWeakAttack(type).Select(x => x.ToString())
            };
        }

        private IEnumerable<string> GetMultiTypeAttackStats(ElementType typeOne, ElementType typeTwo,
            Func<ElementType, IEnumerable<ElementType>> attackFunc)
        {
            var attackOne = attackFunc(typeOne).Select(x => x.ToString()).ToList();
            var attackTwo = attackFunc(typeTwo).Select(x => x.ToString()).ToList();

            return attackOne.Concat(attackTwo).Distinct().Select(x =>
            {
                if (attackOne.Contains(x) && attackTwo.Contains(x))
                {
                    return x;
                }
                if (attackOne.Contains(x))
                {
                    return x + " (" + typeOne.ToString() + ")";
                }
                return x + " (" + typeTwo.ToString() + ")";
            }); 
        }

        private IEnumerable<string> GetMultiTypeStats(IEnumerable<IGrouping<ElementType, ElementType>> typeGroups)
        {
            return typeGroups.Select((typeGroup) =>
            {
                var type = typeGroup.Key.ToString();
                var count = typeGroup.Count();

                if (count > 1)
                {
                    return type.ToString() + " x" + count;
                }
                return type.ToString();
            })
            .OrderBy(x => x);
        }
            
        [UrlPattern("types/getTypes")]
        public GetTypesResponse GetTypes(GetTypesRequest request)
        {
            return new GetTypesResponse
            {
                Types = Enum.GetNames(typeof (ElementType))
                    .ToList()
            };
        }
    }
}