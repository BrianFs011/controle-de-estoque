<template>
  <v-sheet class="mx-auto" width="300">

    <v-form ref="form">
      <v-text-field
        v-model=produto.nome
				:rules="nomeRules"
        label="Nome"
        required
      ></v-text-field>

			<v-text-field
        v-model=produto.preco
        :rules="precoRules"
        label="Preço"
        required
      ></v-text-field>

			<v-text-field
        v-model=produto.quantidade
        :rules="quantidadeRules"
        label="Quantidade"
        required
      ></v-text-field>

			<v-text-field
        v-model=produto.img
        label="URL Imagem"
        required
      ></v-text-field>

			<v-text-field
        v-model=produto.descricao
        label="Descrição"
				:rules="descricaoRules"
        required
      ></v-text-field>

      <div class="d-flex flex-column">
        <v-btn
          class="mt-4"
          color="#ffaaaa"
          block
					to="/"
        >
				<div class="voltar">
					Voltar
				</div>
        </v-btn>

				<v-btn
          class="mt-4"
          color="success"
          block
          @click="cadastrar"
        >
          Cadastrar
        </v-btn>
      </div>
    </v-form>
  </v-sheet>
</template>

<script>
  export default {

    data: () => ({
			produto: {
				nome: '',
				preco: '',
				quantidade: '',
				descricao: '',
				img: ''
			},

      nomeRules: [
        v => !!v || 'Nome é obrigatório',
        v => (v && v.length <= 10) || 'Nome deve ter menos que 10 caracteres',
      ],

			precoRules: [
				v => (v > 0.0 && /[0-9-]+/.test(v)) || 'Preço é obrigatório'
			],

			descricaoRules: [
				v => (v != '')
			],

			quantidadeRules: [
				v => (v > 0) || 'Quantidade é obrigatório'
			]
    }),

    methods: {
			async cadastrar() {
				try
				{

					var response = await this.$axios(
						{
							method: 'POST',
							url: '/PostProduto',
							data: this.produto
						}
					);

				}	catch (error) {
					console.log("Erro: ", error)
				}
			}
    },
  }
</script>

<style>
	.voltar {
		color: white
	}
</style>