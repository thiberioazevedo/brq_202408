export class CDB {
    constructor(
        public mes?: number,
        public valorInicial?: number,
        public valorFinal?: number,
        public imposto?: number,
        public taxaImposto?: number,
        public valorBruto?: number,
        public juros?: number,
        public cDBAnterior?: CDB,
    ) {
    }
}