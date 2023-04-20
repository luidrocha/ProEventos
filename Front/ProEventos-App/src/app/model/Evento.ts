import { Lote } from "./Lote";
import { Palestrante } from "./Palestrante";
import { RedeSocial } from "./RedeSocial";

export interface Evento {

  id: number;
  local: string;
  dataEvento?: string; // ? = pode ser nullo
  tema: string;
  qtdPessoa: number;
  imagemUrl: string;
  telefone: string;
  email: string;
  // Usado para retornar a lista de Lotes e RedeSociais
  lotes: Lote[]; // um evento tem vaios lotes
  redesSociais: RedeSocial[]; // um eventos pode ter varias redes sociais
  // Tabela auxiliar
  palestranteEventos: Palestrante[];
}
