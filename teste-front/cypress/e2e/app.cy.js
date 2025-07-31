class RegistroForm{
  elements ={
    titleInput: () => cy.get('#title'),
    titleFeedback: () => cy.get('#titleFeedback'),
    imagemUrInput: () => cy.get('#imageUrl'),
    imagemUrInputFeedback: () => cy.get('#urlFeedback'),
    submitBtn: () => cy.get('#btnSubmit')
  }

    clickSubmit(){  
      this.elements.submitBtn().click()
    }
    typeTitle(text){
    if(!text) return;
    this.elements.titleInput().type(text)
  }
  
     typeUrl(url){
    if(!url) return;
    this.elements.imagemUrInput().type(url)
    
    
  }
  
}
const registroForm = new RegistroForm();


  describe('Registro de imagem', () => {
  describe('Enviar uma imagem com entradas inválidas', () => {
    const imagem = {
      titulo:'',
      url:''
    }
      
    
    it ('Estou na pagina de cadastro de imagens',() => {
      cy.visit('/')
    })

    it (`Quando eu digito "${imagem.titulo}" no campo do titulo`, () => {
      registroForm.typeTitle(imagem.titulo)

    })

    it (`Quando eu digito "${imagem.url}" no campo de URL`, () => {
      registroForm.typeUrl(imagem.url)
    })
    it ('Eu clico no botao de envio', () => {
      registroForm.clickSubmit()
    })

    it ('Entao eu devo ver a mensagem "Please type a title for the imagem" acima do campo de titulo',() => {
      registroForm.elements.titleFeedback().should("contains.text", "Please type a title for the image")
    })

    it ('E eu devo ver a mensagem "Please type a valid URL" acima do campo de URL da imagem', () => {
      registroForm.elements.imagemUrInputFeedback().should("contains.text", "Please type a valid URL")
    })
  })
  
  describe('Enviar uma imagem com entradas validas', () => {
    const imagem = {
      titulo: 'Monica',
      url: 'https://yt3.googleusercontent.com/PYn2v6l25pYcYz_v6m1doJxi6h0I-vjoAu1qt7BF_EtjwE1qr-n5ewZ4f7TA3WxVgRzfxW2imA=s900-c-k-c0x00ffffff-no-rj'
    }
    
    it('estou na pagina de cadastro de imagem', () => {
      cy.visit('/')
    }) 

    it (`Quando eu digito "${imagem.titulo}" no campo do titulo`, () => {
      registroForm.typeTitle(imagem.titulo)

    })

     it (`Quando eu digito "${imagem.url}" no campo de URL`, () => {
      registroForm.typeUrl(imagem.url)
    })

    it ('Eu clico no botao de envio', () => {
      registroForm.clickSubmit()
    })

     it ('Entao eu devo ver a mensagem "Please type a title for the imagem" acima do campo de titulo',() => {
      registroForm.elements.titleFeedback().should("contains.text", "Please type a title for the image")
    })

    it ('E eu devo ver a mensagem "Please type a valid URL" acima do campo de URL da imagem', () => {
      registroForm.elements.imagemUrInputFeedback().should("contains.text", "Please type a valid URL")
    })
  

  })

})