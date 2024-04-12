<template>
	<v-app>
		<v-main>
			<v-sheet class="mx-auto" width="95%">
				<v-form ref="form">
					<v-text-field
						v-model=pesquisa
						label="Pesquisa"
						required
					></v-text-field>

					<div class="d-flex flex-column">
						<v-btn
							class="mt-4"
							color="primary"
							block
							to="cadastro"
						>
							Cadastrar produto
						</v-btn>
					</div>
				</v-form>
			</v-sheet>

			<v-container fluid mt-8>
				<v-layout row wrap>
					<v-flex
						v-for="item in items" :key="item.id"
						v-if="item.nome.substr(0, pesquisa.length) == pesquisa || item.descricao.substr(0, pesquisa.length) == pesquisa || item.preco == pesquisa"
						xs12
						sm6
						md4
						lg3
						xl3
						pa-4
					>
						<v-card>
							<v-img
								v-if="(idProdEditing != item.id || idProdEditing == '')"
								class="align-end"
								height="250px"
								:src=item.img
							></v-img>

							<v-card-title v-if="(idProdEditing != item.id || idProdEditing == '')">{{item.nome}}</v-card-title>

							<div v-if="(idProdEditing != item.id || idProdEditing == '')" class="d-flex">
								<v-card-text>
									<div>{{ item.descricao }}</div>
									<div>R$ {{ item.preco }}</div>
								</v-card-text>

								<div v-if="(idProdEditing != item.id || idProdEditing == '')" class="my-info">
									<div>Unid</div>
									<div>{{ item.quantidade }}</div>
								</div>
							</div>

							<v-sheet v-if="(idProdEditing != item.id || idProdEditing == '')" class="d-flex flex-column">
								<v-btn
									class="ma-2"
									color="warning"
									@click="editar(item)"
								>Editar</v-btn>
								<v-btn
									class="ma-2"
									color="error"
									@click="remover(item.id)"

								>Remover</v-btn>
							</v-sheet>

							<v-form
								ref="form"
								v-else-if="edit == true && idProdEditing == item.id"
							>
								<v-text-field
									v-model=produto.nome
									:counter="15"
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
									required
								></v-text-field>

								<div class="d-flex flex-column">
									<v-btn
										class="mt-4"
										color="#ffaaaa"
										block
										@click="cancelar"
									>
									<div class="voltar">
										Cancelar
									</div>
									</v-btn>

									<v-btn
										class="mt-4"
										color="success"
										block
										@click="atualizar"
									>
										Atualizar
									</v-btn>
								</div>
							</v-form>

						</v-card>
					</v-flex>
				</v-layout>
			</v-container>
		</v-main>
	</v-app>
</template>

<script>
  export default {
		data() {
			return {
				pesquisa: '',
				items: [],
				id: '',
				edit: false,
				idProdEditing: '',
				produto: {
					nome: '',
					preco: '',
					quantidade: '',
					descricao: '',
					img: ''
				},

				nomeRules: [
					v => !!v || 'Nome é obrigatório',
					v => (v && v.length <= 15) || 'Nome deve ter menos que 10 caracteres',
				],

				precoRules: [
					v => (v > 0.0 && /[0-9-]+/.test(v)) || 'Preço é obrigatório'
				],

				quantidadeRules: [
					v => (v > 0) || 'Quantidade é obrigatório'
				]

			}
		},

		async beforeMount(){
			this.items = await this.$axios.$get('/GetListaProduto')
			this.items.reverse()
		},

    methods: {
			async remover(id) {
				try
				{
					var response = await this.$axios(
						{
							method: 'POST',
							url: '/PostProdutoDelete',
					 		data: new String(id)
					 	}
					);
					this.items = await this.$axios.$get('/GetListaProduto')
					this.items.reverse()
				}	catch (error) {
					console.log("Erro: ", error)
				}
			},
			editar(Produto) {
				this.edit = true
				this.idProdEditing = Produto.id
				this.produto.id = Produto.id
				this.produto.nome = Produto.nome
				this.produto.descricao = Produto.descricao
				this.produto.img = Produto.img
				this.produto.preco = Produto.preco
				this.produto.quantidade = Produto.quantidade
			},
			async atualizar() {
				try
				{
					var response = await this.$axios(
						{
							method: 'POST',
							url: '/PostAtualizaProduto',
					 		data: this.produto
					 	}
					);
					this.items = await this.$axios.$get('/GetListaProduto')
					this.items.reverse()

					this.edit = false
					this.idProdEditing = ''
					this.produto.nome = ''
					this.produto.descricao = ''
					this.produto.img = ''
					this.produto.preco = ''
					this.produto.quantidade = ''
				}	catch (error) {
					console.log("Erro: ", error)
				}
			},
			cancelar() {
				this.edit = false
				this.idProdEditing = ''
				this.produto.nome = ''
				this.produto.descricao = ''
				this.produto.img = ''
				this.produto.preco = ''
				this.produto.quantidade = ''
			},
			uppesquisa(newPesquisa)
			{
				this.pesquisa = newPesquisa
			}
    },
  }

</script>

<style>
	.my-info{
		align-self: center;
		margin-right: 20px
	}
	.voltar {
		color: white
	}
</style>