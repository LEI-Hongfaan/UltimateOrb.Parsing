using System;
using System.Collections.Generic;
using System.Text;

namespace UltimateOrb.Parsing.Generic {

    struct CharSyntax<TChar>
        : ICharSyntax<TChar, (int Start, int Length)> {

        public ISyntaxExpression GetDefination() {
            throw new NotImplementedException();
        }

        public ISyntaxParser GetParser() {
            throw new NotImplementedException();
        }
    }
}
