�����̃I�u�W�F�N�g�v�[���̓���
�悭����I�u�W�F�N�g���A�N�e�B�u��Ԃɂ��ăv�[���B
�v���n�u��InstanceID�ŊǗ�����̂Ń��X�g�̓o�^���s�v�B
�Е��������N���X�g�Ȃ̂ō����B
�������̃X�N���v�g�̕ύX�����Ȃ��B
�v�[���������Ǘ�����ׁA���ʂ�Destroy���Ă��e���Ȃ��B
�V�[���ɒ��ڔz�u�����ꍇ�̓v�[��������Destroy����B
�v���n�u���Ƃɏ������ƃv�[�����̃C�x���g��o�^�B
�I�u�W�F�N�g�v�[���}�l�[�W���̃T���v����t���B

���t�@�C���\��
ObjectPool.cs �F�I�u�W�F�N�g�v�[���{�̂̃X�N���v�g(�V���O���g��)
ObjectPoolItem.cs �F�v�[���������v���n�u�̃��[�g�ɒǉ�����X�N���v�g
ObjectPoolManager_sample.cs �F�v�[�����鐔�𒲐�����X�N���v�g�̃T���v��
readme.txt �F�I�u�W�F�N�g�v�[���̐���

���g����
�P�A�v���n�u��ObjectPoolItem�R���|�[�l���g��ǉ�����B

�Q�A�v���n�u������using A_rosuko.ObjectPool;��ǉ�����
Instantiate(�v���n�u, pos , qt);�����L�ɏ��������Ă��������B
ObjectPool.Instance.GetGameObject(�v���n�u , pos , qt);

�R�A�v�[����using A_rosuko.ObjectPool;��ǉ�����
Destroy(gameObject);�����L�ɏ���������B
ObjectPool.Instance.SleepGameObject(ObjectPoolItem);

��ObjectPool.Instance.SleepGameObject(gameObject)�ł����삵�܂����A
������GetConponent<ObjectPoolItem>()���Ă���̂�ObjectPoolItem���L���b�V�����ēn�����������ł��B

�S�A�������Ȃǂ��K�v�ł���΃G�f�B�^�̃C���X�y�N�^�[����OnEventWakeUp��OnEventSleep�C�x���g�Ɋ֐���o�^���Ă��������B

���d�g��
�I�u�W�F�N�g�v�[���{�̂�Dictionary�Ń����N���X�g�̐擪���쐬����B
Dictionary�̃L�[�̓v���n�u��InstanceID�Ƃ���B
�e�I�u�W�F�N�g��ObjectPoolItem�R���|�[�l���g���Őڑ����郊���N���X�g�̐擪���L���b�V�����Ă����A�擪�̎��ɐڑ�����B
�v�[��������o�����̓����N���X�g�̐擪�̎�������o���A���̎���擪�ɐڑ����Ȃ����B

���ۏ؋y�ѐӔC�A���쌠�ɂ���
�{�\�t�g�E�F�A�͖��ۏ؂ł��B
�{�\�t�g�E�F�A�̎g�p��ʂ��Đ����������Ȃ鑹�Q�A�s���v�ɑ΂��Ă��A����҂͐ӔC�𕉂�Ȃ����̂Ƃ��܂��B

ObjactPool�FCopyright 2018 A_rosuko
Released under the MIT license