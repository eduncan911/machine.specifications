using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.UnitTestExplorer;

using Machine.Specifications.ReSharperRunner.Factories;
using Machine.Specifications.ReSharperRunner.Presentation;

namespace Machine.Specifications.ReSharperRunner.Explorers.ElementHandlers
{
  internal class BehaviorElementHandler : IElementHandler
  {
    readonly BehaviorFactory _behaviorFactory;

    public BehaviorElementHandler(BehaviorFactory behaviorFactory)
    {
      _behaviorFactory = behaviorFactory;
    }

    #region Implementation of IElementHandler
    public bool Accepts(IElement element)
    {
      IDeclaration declaration = element as IDeclaration;
      if (declaration == null)
      {
        return false;
      }

      return declaration.DeclaredElement.IsBehavior();
    }

    public UnitTestElementDisposition AcceptElement(IElement element, IFile file)
    {
      IDeclaration declaration = (IDeclaration) element;
      Element unitTestElement = _behaviorFactory.CreateBehaviorElement(declaration.DeclaredElement);

      return new UnitTestElementDisposition(unitTestElement,
                                            file.ProjectFile,
                                            declaration.GetNameRange(),
                                            declaration.GetDocumentRange().TextRange);
    }
    #endregion
  }
}